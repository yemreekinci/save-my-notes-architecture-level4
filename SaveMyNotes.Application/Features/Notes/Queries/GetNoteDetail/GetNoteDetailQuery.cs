using MediatR;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Notes.Queries.GetNoteDetail;

public record GetNoteDetailQuery(Guid Id) : IRequest<NoteDetailDTO>;