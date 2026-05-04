using Microsoft.EntityFrameworkCore;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;
using SaveMyNotes.Infrastructure.Persistence.Context;

namespace SaveMyNotes.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    // DI
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context) => _context = context;

    // Implementation
    public async Task<IEnumerable<Category>> GetAllAsync(Guid userId)
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
    public async Task<Category?> GetByIdAsync(Guid id) => await _context.Categories.FindAsync(id);
    public async Task AddAsync(Category category) => await _context.Categories.AddAsync(category);
    public void Update(Category category) => _context.Categories.Update(category);
    public void Delete(Category category) => _context.Categories.Remove(category);
}

