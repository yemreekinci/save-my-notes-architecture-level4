using Microsoft.EntityFrameworkCore;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;
using SaveMyNotes.Domain.Enums;
using SaveMyNotes.Infrastructure.Persistence.Context;

namespace SaveMyNotes.Infrastructure.Persistence.Repositories;

public class NoteRepository : INoteRepository
{
    // DI
    private readonly AppDbContext _context;
    public NoteRepository(AppDbContext context) => _context = context;

    // Implementation
    public async Task<IEnumerable<Note>> GetAllAsync(Guid userId)
    {
        return await _context.Notes
            .Include(n => n.Category)
            .Include(n => n.Tags)  
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
    public async Task<Note?> GetByIdAsync(Guid id)
    {
        return await _context.Notes
            .Include(n => n.Category)
            .Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    public async Task AddAsync(Note note) => await _context.Notes.AddAsync(note);
    public void Update(Note note) => _context.Notes.Update(note);
    public void Delete(Note note) => _context.Notes.Remove(note);

    public async Task DeleteAllByUserIdAsync(Guid userId)
    {
        await _context.Notes
                .Where(n => n.UserId == userId && n.Status != NoteStatus.Deleted)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(n => n.Status, NoteStatus.Deleted)
                );
    }
}

