using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Interfaces;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(Guid id);
    Task<IEnumerable<Note>> GetAllAsync(Guid UserId);
    Task AddAsync(Note note);
    void Update(Note note);
    void Delete(Note note);
    Task DeleteAllByUserIdAsync(Guid userId);
}
