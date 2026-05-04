using SaveMyNotes.Application.DTOs;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Mappings
{
    public static class NoteMappingExtensions
    {
        public static NoteDTO MapToDto(this Note note)
        {
            return new NoteDTO(
                note.Id,
                note.Title,
                note.Content,
                note.Category?.Name ?? "Uncategorized",
                note.Category?.Color?.Color ?? "#FFFFFF",
                note.Tags.Select(t => t.Name).ToList(),
                note.CreatedAt
            );
        }
        public static NoteDetailDTO MapToDetailDto(this Note note)
        {
            return new NoteDetailDTO(
                Id: note.Id,
                Title: note.Title,
                Content: note.Content,
                CategoryId: note.CategoryId,
                CategoryName: note.Category?.Name ?? "Kategorisiz",
                Tags: note.Tags.Select(tag => tag.MapToDto()).ToList(),
                CreatedAt: note.CreatedAt,
                UpdatedAt: note.UpdatedAt
            );
        }
    }
}
