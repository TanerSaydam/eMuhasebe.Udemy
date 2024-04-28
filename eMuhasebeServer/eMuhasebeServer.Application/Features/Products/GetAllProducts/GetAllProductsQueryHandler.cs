using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.GetAllProducts;

internal sealed class GetAllProductsQueryHandler(
    IProductRepository productRepository,
    ICacheService cacheService) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product>? products = cacheService.Get<List<Product>>("products");

        if(products is null)
        {
            products = await productRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);

            cacheService.Set("products", products);
        }

        return products;
    }
}
