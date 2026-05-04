using MediatR;

namespace SaveMyNotes.Application.Features.Notes.Commands.CreateNote;

public record CreateNoteCommand(string Title,
                                string Content,
                                Guid CategoryId) : IRequest<Guid>;
