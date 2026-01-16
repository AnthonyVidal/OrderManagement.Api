using OrderManagement.Domain.Exceptions;
namespace OrderManagement.Domain
{
    public class OrderDetail
    {
        public Guid Id { get; private set; }
        public string Producto { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public OrderDetail(string producto, int cantidad, decimal precio)
        {
            if (cantidad <= 0 || precio <= 0)
                throw new DomainException("Cantidad y precio deben ser mayores a cero.");

            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precio;
        }

        // Constructor requerido por EF Core
        private OrderDetail() { }
    }

}
