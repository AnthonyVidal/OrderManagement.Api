using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Domain;

namespace OrderManagement.Application.Orders.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Fecha = order.Fecha,
                Cliente = order.Cliente,
                Total = order.Total,
                Details = order.Details
                    .Select(ToDetailDto)
                    .ToList()
            };
        }

        private static OrderDetailDto ToDetailDto(OrderDetail detail)
        {
            return new OrderDetailDto
            {
                Producto = detail.Producto,
                Cantidad = detail.Cantidad,
                PrecioUnitario = detail.PrecioUnitario,
                Subtotal = detail.Subtotal
            };
        }
    }
}
