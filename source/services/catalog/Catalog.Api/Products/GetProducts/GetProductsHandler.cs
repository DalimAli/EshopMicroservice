﻿

using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

public record  GetProductsQuery(int PageNumber, int PageSize) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken) 
    {
        var products = await documentSession.Query<Product>()
            .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetProductsResult(products);
    }
}

