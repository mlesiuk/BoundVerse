using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BoundVerse.Domain.Entities;

namespace BoundVerse.Infrastructure.Persistence.Configurations;

public sealed class BookConfiguration : BaseConfiguration, IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .OwnsMany(book => book.Author);
            
    }
}