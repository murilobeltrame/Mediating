using Domain.TodoAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Db.Configurations;

internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.Removed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(t => t.DueDate)
            .HasColumnType("timestamp without time zone");

        builder.Property(t => t.CompletedAt)
            .HasColumnType("timestamp without time zone");

        builder.OwnsOne(t => t.Coordinates);
    }
}
