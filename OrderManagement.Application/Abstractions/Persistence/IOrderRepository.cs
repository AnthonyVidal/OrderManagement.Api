using OrderManagement.Domain;

namespace OrderManagement.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);

        Task<Order?> GetByIdAsync(Guid id);

        Task<bool> ExistsByClientAndDate(string cliente, DateTime fecha);

        Task<IReadOnlyList<Order>> GetPagedAsync(
            string? cliente,
            DateTime? desde,
            DateTime? hasta,
            int page,
            int pageSize);
       
        Task SaveChangesAsync();

        void Remove(Order order);
    }
}
