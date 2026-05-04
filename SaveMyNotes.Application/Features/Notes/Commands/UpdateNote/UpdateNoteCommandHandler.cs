using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Features.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Guid>
{
    private readonly INoteRepository _noteRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IAIService _aiService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateNoteCommandHandler(
        INoteRepository noteRepository,
        ITagRepository tagRepository,
        IAIService aiService,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _noteRepository = noteRepository;
        _tagRepository = tagRepository;
        _aiService = aiService;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null || note.UserId != userId)
        {
            throw new Exception("Not bulunamadı veya bu işlem için yetkiniz yok.");
        }

        note.UpdateTitle(request.Title);
        note.UpdateContent(request.Content);

        var extractedTagNames = await _aiService.ExtractTagsAsync(request.Content);

        note.Tags.Clear();

        if (extractedTagNames != null)
        {
            foreach (var tagName in extractedTagNames)
            {
                var existingTag = (await _tagRepository.GetByNameAsync(tagName)).FirstOrDefault();
                if (existingTag != null)
                {
                    note.Tags.Add(existingTag);
                }
                else
                {
                    note.Tags.Add(new Tag(tagName));
                }
            }
        }

        _noteRepository.Update(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}