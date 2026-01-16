using MediatR;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Domain;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsByClientAndDate(
                request.Cliente, request.Fecha);

            if (exists)
                throw new DomainException(
                    "El cliente ya tiene una orden registrada en esta fecha.");

            var details = request.Details
                .Select(d => new OrderDetail(
                    d.Producto, d.Cantidad, d.PrecioUnitario))
                .ToList();

            var order = new Order(request.Fecha, request.Cliente, details);

            await _repository.AddAsync(order);

            return order.Id;
        }
    }
}
