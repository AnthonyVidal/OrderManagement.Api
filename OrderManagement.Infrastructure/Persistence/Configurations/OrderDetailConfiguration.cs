using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Producto)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(d => d.PrecioUnitario)
                   .HasPrecision(18, 2);
        }
    }
}
