using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Application.Common.Mappings;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Notes.Queries.GetNotesList;

public class GetNotesListHandler : IRequestHandler<GetNotesListQuery, IEnumerable<NoteDTO>>
{
    private readonly INoteRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    public GetNotesListHandler(INoteRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<NoteDTO>> Handle(GetNotesListQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));
        var notes = await _repository.GetAllAsync(userId);
        return notes.Select(n => n.MapToDto());
    }
}