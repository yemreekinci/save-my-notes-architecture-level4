using MediatR;

namespace SaveMyNotes.Application.Features.Notes.Commands.UpdateNote;

public record UpdateNoteCommand(Guid Id,
                                string Title,
                                string Content,
                                Guid CategoryId) : IRequest<Guid>;