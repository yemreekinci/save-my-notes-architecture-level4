namespace SaveMyNotes.Application.DTOs;

public record NoteDTO(Guid Id,
                    string Title,
                    string Content,
                    string CategoryName,
                    string CategoryColor,
                    List<string> Tags, 
                    DateTime CreatedAt);