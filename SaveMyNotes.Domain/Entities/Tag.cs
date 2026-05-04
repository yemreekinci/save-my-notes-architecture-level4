using SaveMyNotes.Domain.Common;
using SaveMyNotes.Domain.Enums;
using SaveMyNotes.Domain.Exceptions;
using System.Net.NetworkInformation;

namespace SaveMyNotes.Domain.Entities;

public class Tag : BaseEntity
{
    // Props
    public string Name { get; private set; } = string.Empty;
    public NoteStatus Status { get; private set; } = NoteStatus.Default;

    // Relations
    public virtual ICollection<Note> Notes { get; private set; } = new HashSet<Note>();

    // EFCore
    private Tag() { }

    // Logics
    public Tag(string name)
    {
        UpdateName(name);
        Status = NoteStatus.Default;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NoteValidationException("Tag name cannot be empty");
        }
        Name = name;
        Status = NoteStatus.Modified;
        UpdatedAt = DateTime.UtcNow;
    }
}
