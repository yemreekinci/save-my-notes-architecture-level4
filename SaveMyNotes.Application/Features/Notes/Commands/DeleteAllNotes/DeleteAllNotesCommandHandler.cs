using MediatR;
using SaveMyNotes.Application.Common.Interfaces;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteAllNotes;

public class DeleteAllNotesCommandHandler : IRequestHandler<DeleteAllNotesCommand>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteAllNotesCommandHandler(INoteRepository noteRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteAllNotesCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(currentUserId))
            throw new UnauthorizedAccessException("Geçerli bir oturum bulunamadı.");

        var userId = Guid.Parse(currentUserId);

        await _noteRepository.DeleteAllByUserIdAsync(userId);
        await _unitOfWork.SaveChangesAsync();
    }
}