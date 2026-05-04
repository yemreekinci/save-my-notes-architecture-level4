using SaveMyNotes.Domain.Common;
using SaveMyNotes.Domain.Enums;
using SaveMyNotes.Domain.Exceptions;
using SaveMyNotes.Domain.ValueObjects;

namespace SaveMyNotes.Domain.Entities;

public class Note : BaseEntity
{
    // Props
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public NoteStatus Status { get; private set; } = NoteStatus.Default;
    public DisplayOrder Order { get; private set; }

    // Relations
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public virtual Category Category { get; private set; } = null!;
    public virtual ICollection<Tag> Tags { get; private set; } = new HashSet<Tag>();

    // EFCore
    private Note(){}

    // Logics
    public Note(string title, string content, Guid userId, Guid categoryId, int initialOrder)
    {
        UpdateTitle(title);
        UpdateContent(content);
        UserId = userId;
        CategoryId = categoryId;
        Order = new DisplayOrder(initialOrder);
        Status = NoteStatus.Default;
    }

    public void UpdateContent(string content)
    {
        Content = content ?? string.Empty;
        Status = NoteStatus.Modified;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new NoteValidationException("Title cannot be empty");
        }
        Title = title;
        Status = NoteStatus.Modified;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateOrder(int newOrder)
    {
        Order = new DisplayOrder(newOrder);
        Status = NoteStatus.Modified;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        Status = NoteStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;
    }
}
