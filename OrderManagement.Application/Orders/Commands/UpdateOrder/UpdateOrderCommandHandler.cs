using MediatR;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Application.Common.Exceptions;

namespace OrderManagement.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler
    : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _repo;

        public UpdateOrderCommandHandler(IOrderRepository repository)
        {
            _repo = repository;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken ct)
        {
            var order = await _repo.GetByIdAsync(request.Id);
            if (order == null)
                throw new NotFoundException("Orden no encontrada");

            order.UpdateCliente(request.Cliente);

            await _repo.SaveChangesAsync();
        }
    }


}
