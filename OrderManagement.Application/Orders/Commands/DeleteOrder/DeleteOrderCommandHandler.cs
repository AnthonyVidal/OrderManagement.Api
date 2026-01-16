using MediatR;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Application.Common.Exceptions;

namespace OrderManagement.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler
    : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _repo;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repo = repository;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken ct)
        {
            var order = await _repo.GetByIdAsync(request.Id);
            if (order == null)
                throw new NotFoundException("Orden no encontrada");

            _repo.Remove(order);
            await _repo.SaveChangesAsync();
        }
    }


}
