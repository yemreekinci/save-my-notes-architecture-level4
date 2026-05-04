using Microsoft.EntityFrameworkCore;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;
using SaveMyNotes.Infrastructure.Persistence.Context;

namespace SaveMyNotes.Infrastructure.Persistence.Repositories;
public class TagRepository : ITagRepository
{   
    // DI
    private readonly AppDbContext _context;
    public TagRepository(AppDbContext context) => _context = context;

    // Implementation
    public async Task<IEnumerable<Tag>> GetByNameAsync(string name)
    {
        return await _context.Tags
            .Where(t => t.Name.Contains(name))
            .ToListAsync();
    }
    public async Task<Tag?> GetByIdAsync(Guid id) => await _context.Tags.FindAsync(id);
    public async Task AddAsync(Tag tag) => await _context.Tags.AddAsync(tag);
    public void Delete(Tag tag) => _context.Tags.Remove(tag);
    public async Task<IEnumerable<Tag>> GetAllAsync(Guid userId) => await _context.Tags.ToListAsync();
}
