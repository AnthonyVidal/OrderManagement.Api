using MediatR;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Application.Orders.Queries.GetOrders;
using OrderManagement.Application.Orders.Mappers;

public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repo;

    public GetOrdersQueryHandler(IOrderRepository repo)
    {
        _repo = repo ?? throw new Exception("IOrderRepository NO inyectado");
    }


    public async Task<List<OrderDto>> Handle(GetOrdersQuery q, CancellationToken ct)
    {
        var orders = await _repo.GetPagedAsync(
            q.Cliente, q.Desde, q.Hasta, q.Page, q.PageSize);

        return orders.Select(OrderMapper.ToDto).ToList();
    }
}

