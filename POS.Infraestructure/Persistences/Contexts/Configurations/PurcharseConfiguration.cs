using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;


namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    public class PurcharseConfiguration : IEntityTypeConfiguration<Purcharse>
    {
        public void Configure(EntityTypeBuilder<Purcharse> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PurcharseId");

            builder.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Igv).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

        }
    }
}
