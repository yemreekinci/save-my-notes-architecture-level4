using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Application.Common.Mappings;
using SaveMyNotes.Application.DTOs;
using SaveMyNotes.Application.Features.Notes.Queries.GetNoteDetail;

public class GetNoteDetailHandler : IRequestHandler<GetNoteDetailQuery, NoteDetailDTO>
{
    private readonly INoteRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    public GetNoteDetailHandler(INoteRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<NoteDetailDTO> Handle(GetNoteDetailQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));
        var note = await _repository.GetByIdAsync(request.Id);

        if (note == null || note.UserId != userId)
            throw new Exception("Not bulunamadı.");

        return note.MapToDetailDto();
    }
}