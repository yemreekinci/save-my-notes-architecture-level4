using SaveMyNotes.Domain.Exceptions;

namespace SaveMyNotes.Domain.ValueObjects;

public record DisplayColor
{
    public string Color { get; init; }
    public DisplayColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color) || (color.Length != 7 && color.Length != 4) || color[0] != '#')
        {
            throw new NoteValidationException("Invalid color code");
        }
        Color = color;
    }
}
