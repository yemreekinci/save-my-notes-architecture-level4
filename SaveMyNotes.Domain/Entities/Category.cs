using SaveMyNotes.Domain.Common;
using SaveMyNotes.Domain.Enums;
using SaveMyNotes.Domain.Exceptions;
using SaveMyNotes.Domain.ValueObjects;
using System.Net.NetworkInformation;

namespace SaveMyNotes.Domain.Entities;

public class Category : BaseEntity
{
    // Props
    public string Name { get; private set; } = string.Empty;
    public DisplayColor Color { get; private set; } = new DisplayColor("#FFFFFF");
    public NoteStatus Status { get; private set; } = NoteStatus.Default;

    // Relations
    public virtual ICollection<Note> Notes { get; private set; } = new HashSet<Note>();

    // EFCore
    private Category() { }

    // Logics
    public Category(string name, string color)
    {
        UpdateName(name);
        Color = new DisplayColor(color);
        Status = NoteStatus.Default;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new NoteValidationException("Category name cannot be empty");
        }
        Name = name;
        Status = NoteStatus.Modified;
        UpdatedAt = DateTime.UtcNow;
    }
}
