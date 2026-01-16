using MediatR;
using OrderManagement.Application.Orders.Dtos;

namespace OrderManagement.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(
    string? Cliente,
    DateTime? Desde,
    DateTime? Hasta,
    int Page = 1,
    int PageSize = 10
) : IRequest<List<OrderDto>>;
}
