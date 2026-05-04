using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Infrastructure.Persistence.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title).IsRequired().HasMaxLength(100);
        builder.Property(n => n.Content).IsRequired();

        // Value Object
        builder.OwnsOne(x => x.Order, order =>
        {
            order.Property(o => o.Order).HasColumnName("DisplayOrder").IsRequired();
        });

        // Relations
        builder.HasOne(n => n.Category)
            .WithMany(c => c.Notes)
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(n => n.Tags)
            .WithMany(t => t.Notes);

        // Soft Delete Filter
        builder.HasQueryFilter(n => n.Status != Domain.Enums.NoteStatus.Deleted);
    }
}