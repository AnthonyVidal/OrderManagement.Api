using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Cliente)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.Total)
                   .HasPrecision(18, 2);

            builder.HasMany(o => o.Details)
                   .WithOne()
                   .HasForeignKey("OrderId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
