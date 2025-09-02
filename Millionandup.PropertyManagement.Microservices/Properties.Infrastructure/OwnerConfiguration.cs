using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain.Entities;

namespace Properties.Infrastructure;


/// <summary>
/// configuracion de entidades para mapear a bd
/// </summary>
public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(e => e.IdOwner);

        builder.ToTable("Owner");

        builder.Property(e => e.IdOwner).ValueGeneratedOnAdd();
        builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
        builder.Property(e => e.Birthday)
            .HasColumnType("datetime");
        builder.Property(e => e.Document);

        builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

        builder.Property(e => e.Photo)
                .HasColumnType("image");
    }
}