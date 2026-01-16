using OrderManagement.Domain.Exceptions;
namespace OrderManagement.Domain
{
    public class Order
    {
        public Guid Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Cliente { get; private set; }
        public decimal Total { get; private set; }
        public List<OrderDetail> Details { get; private set; }

        private Order() { }

        public Order(DateTime fecha, string cliente, List<OrderDetail> details)
        {
            if (!details.Any())
                throw new DomainException("La orden debe tener al menos un detalle.");

            Fecha = fecha;
            Cliente = cliente;
            Details = details;            
            Total = details.Sum(d => d.Subtotal);
        }

        public void UpdateCliente(string cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente))
                throw new DomainException("El cliente no puede ser vacío.");

            Cliente = cliente;
        }

    }

}
