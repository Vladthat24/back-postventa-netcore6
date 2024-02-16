using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    public class PurcharseDetailConfiguration : IEntityTypeConfiguration<PurcharseDetail>
    {
        public void Configure(EntityTypeBuilder<PurcharseDetail> builder)
        {
            builder.HasKey(e => new { e.PurcharseId, e.ProductId });
            builder.Property(e => e.UnitPurcharsePrice).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Total).HasColumnType("decimal(18,2)");

        }
    }
}
