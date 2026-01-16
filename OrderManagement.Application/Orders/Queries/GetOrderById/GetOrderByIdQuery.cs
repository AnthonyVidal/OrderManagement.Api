using OrderManagement.Application.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderManagement.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto>;

}
