using FluentValidation;

namespace SaveMyNotes.Application.Features.Notes.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık 100 karakterden uzun olamaz.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Not içeriği boş olamaz.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Bir kategori seçilmelidir.");
    }
}