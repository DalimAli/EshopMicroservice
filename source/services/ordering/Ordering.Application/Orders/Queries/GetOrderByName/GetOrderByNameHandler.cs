namespace Ordering.Application.Orders.Queries.GetOrderByName;

internal class GetOrderByNameQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        // get orders by name using db context
        // return the result


        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrderByNameResult(orders.ToOrderDtoList());
    }


}
