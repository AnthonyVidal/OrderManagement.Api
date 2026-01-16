using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Domain;
using OrderManagement.Infrastructure.Persistence.DbContexts;

namespace OrderManagement.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;

        public OrderRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> ExistsByClientAndDate(
            string cliente, DateTime fecha)
        {
            return await _context.Orders.AnyAsync(o =>
                o.Cliente == cliente &&
                o.Fecha.Date == fecha.Date);
        }


        public async Task<IReadOnlyList<Order>> GetPagedAsync(
            string? cliente,
            DateTime? desde,
            DateTime? hasta,
            int page,
            int pageSize)
        {
            var query = _context.Orders
                .Include(o => o.Details)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(cliente))
                query = query.Where(o => o.Cliente == cliente);

            if (desde.HasValue)
                query = query.Where(o => o.Fecha >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(o => o.Fecha <= hasta.Value);

            return await query
                .OrderByDescending(o => o.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }
    }
}
