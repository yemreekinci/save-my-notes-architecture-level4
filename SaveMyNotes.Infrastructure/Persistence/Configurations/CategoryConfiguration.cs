using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        // Value Object
        builder.OwnsOne(x => x.Color, color =>
        {
            color.Property(c => c.Color).HasColumnName("Color").IsRequired();
        });
    }
}