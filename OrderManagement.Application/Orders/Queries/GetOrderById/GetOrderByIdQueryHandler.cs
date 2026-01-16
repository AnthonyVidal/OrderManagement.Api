using MediatR;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Application.Common.Exceptions;
using OrderManagement.Application.Orders.Dtos;

namespace OrderManagement.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler
        : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderDto> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            if (order is null)
                throw new NotFoundException("Orden no encontrada.");

            return new OrderDto
            {
                Id = order.Id,
                Fecha = order.Fecha,
                Cliente = order.Cliente,
                Total = order.Total,
                Details = order.Details.Select(d => new OrderDetailDto
                {
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }
    }
}
