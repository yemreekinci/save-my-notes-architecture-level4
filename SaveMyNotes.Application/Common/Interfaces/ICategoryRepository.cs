using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(Guid Id);
    Task<IEnumerable<Category>> GetAllAsync(Guid UserId);
    Task AddAsync(Category category);
    void Update(Category category);
    void Delete(Category category);
}
