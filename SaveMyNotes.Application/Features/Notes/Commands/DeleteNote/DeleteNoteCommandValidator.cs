using FluentValidation;
using SaveMyNotes.Application.Common.Interfaces;

namespace SaveMyNotes.Application.Features.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator(ICurrentUserService currentUserService)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Silinecek notun ID'si belirtilmelidir.");
    }
}