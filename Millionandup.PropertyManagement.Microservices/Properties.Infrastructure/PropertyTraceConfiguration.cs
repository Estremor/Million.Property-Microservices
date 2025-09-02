using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain.Entities;

namespace Properties.Infrastructure;

public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
{
    public void Configure(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder.HasKey(e => e.IdPropertyTrace);

        builder.ToTable("PropertyTrace");

        builder.Property(e => e.IdPropertyTrace)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DataSale)
                .HasMaxLength(500)
                .IsUnicode(false);

        builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

        builder.Property(e => e.Tax)
            .HasColumnType("decimal(18, 2)");

        builder.Property(e => e.Value)
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(d => d.IdPropertyNavigation)
                .WithMany(p => p.PropertyTraces)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyTrace_Property");
    }
}
