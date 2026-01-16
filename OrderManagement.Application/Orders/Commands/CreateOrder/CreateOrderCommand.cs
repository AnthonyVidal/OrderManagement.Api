using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
    DateTime Fecha,
    string Cliente,
    List<CreateOrderDetailDto> Details
    ) : IRequest<Guid>;

}
