using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;

namespace FCG.Infra.Data.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("games");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                   .ValueGeneratedOnAdd();

            builder.HasIndex(j => j.Title)
                    .IsUnique();

            builder.Property(j => j.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(j => j.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(j => j.Genre)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(j => j.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        }
    }
}
