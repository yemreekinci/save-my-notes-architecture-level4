namespace SaveMyNotes.Application.DTOs;

// Response
public record NoteDetailDTO(Guid Id,
                            string Title,
                            string Content,
                            Guid CategoryId,
                            string CategoryName,
                            List<TagDTO> Tags,
                            DateTime CreatedAt,
                            DateTime? UpdatedAt);

