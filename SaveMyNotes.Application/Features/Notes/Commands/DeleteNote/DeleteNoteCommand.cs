using MediatR;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(Guid Id) : IRequest;