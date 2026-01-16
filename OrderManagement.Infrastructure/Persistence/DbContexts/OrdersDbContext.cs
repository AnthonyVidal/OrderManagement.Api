using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderManagement.Infrastructure.Persistence.DbContexts
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OrdersDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
