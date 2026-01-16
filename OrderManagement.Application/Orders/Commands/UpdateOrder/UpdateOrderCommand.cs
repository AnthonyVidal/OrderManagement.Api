using MediatR;

namespace OrderManagement.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(Guid Id, string Cliente) : IRequest;

}
