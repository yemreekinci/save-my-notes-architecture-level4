using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Category>().HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Genel",
                Status = Domain.Enums.NoteStatus.Default,
                CreatedAt = seedDate
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "İş",
                Status = Domain.Enums.NoteStatus.Default,
                CreatedAt = seedDate
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Kişisel",
                Status = Domain.Enums.NoteStatus.Default,
                CreatedAt = seedDate
            }
        );

        modelBuilder.Entity<Category>().OwnsOne(x => x.Color).HasData(
            new { CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Color = "#808080" },
            new { CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Color = "#0078D4" },
            new { CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Color = "#28A745" }
        );

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}