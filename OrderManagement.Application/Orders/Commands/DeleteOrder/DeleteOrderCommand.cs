using MediatR;

namespace OrderManagement.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid Id) : IRequest;

}
