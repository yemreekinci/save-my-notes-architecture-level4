using FluentValidation;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteAllNotes;

public class DeleteAllNotesCommandValidator : AbstractValidator<DeleteAllNotesCommand>
{
    public DeleteAllNotesCommandValidator()
    {

    }
}