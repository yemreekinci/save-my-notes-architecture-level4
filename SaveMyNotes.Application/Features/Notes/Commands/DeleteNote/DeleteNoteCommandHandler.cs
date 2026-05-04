using MediatR;
using SaveMyNotes.Application.Common.Interfaces;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteNoteCommandHandler(INoteRepository noteRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
            throw new UnauthorizedAccessException("Oturum açmış kullanıcı bulunamadı.");

        var userId = Guid.Parse(currentUserId);
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null || note.UserId != userId)
        {
            throw new Exception("Not bulunamadı veya bu işlem için yetkiniz yok.");
        }

        note.SoftDelete();
        _noteRepository.Update(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}