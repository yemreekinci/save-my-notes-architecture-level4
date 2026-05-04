using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Interfaces;

public interface ITagRepository
{
    Task<Tag?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tag>> GetAllAsync(Guid UserId);
    Task<IEnumerable<Tag>> GetByNameAsync(string name);
    Task AddAsync(Tag tag);
    void Delete(Tag tag);
}