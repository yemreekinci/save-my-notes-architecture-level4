using FluentValidation;

namespace SaveMyNotes.Application.Features.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}