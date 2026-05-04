using MediatR;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteAllNotes;

public record DeleteAllNotesCommand() : IRequest;