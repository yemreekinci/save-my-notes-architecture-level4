using MediatR;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Notes.Queries.GetNotesList;

public record GetNotesListQuery() : IRequest<IEnumerable<NoteDTO>>;