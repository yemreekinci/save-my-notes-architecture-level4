using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Features.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INoteRepository _noteRepository;
    private readonly ITagRepository _tagRepository; 
    private readonly IAIService _aiService;         
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateNoteCommandHandler(
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

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));

        var note = new Note(
            request.Title,
            request.Content,
            userId,
            request.CategoryId,
            0
        );

        var extractedTagNames = await _aiService.ExtractTagsAsync(request.Content);

        if (extractedTagNames != null && extractedTagNames.Any())
        {
            var uniqueTags = extractedTagNames.Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var tagName in uniqueTags)
            {
                var tagList = await _tagRepository.GetByNameAsync(tagName);
                var existingTag = tagList.FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

                if (existingTag != null)
                {
                    note.Tags.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag(tagName);
                    note.Tags.Add(newTag);
                }
            }
        }

        await _noteRepository.AddAsync(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}