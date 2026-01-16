using OrderManagement.Domain;
using OrderManagement.Infrastructure.Persistence.DbContexts;

namespace OrderManagement.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(OrdersDbContext context)
        {
            if (context.Orders.Any())
                return;

            var order = new Order(
                DateTime.Today,
                "Cliente Demo",
                new List<OrderDetail>
                {
                    new("Producto A", 2, 50),
                    new("Producto B", 1, 100)
                }
            );

            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}
