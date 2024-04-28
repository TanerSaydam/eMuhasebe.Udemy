using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        if(product.Name != request.Name)
        {
            bool isNameExists = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isNameExists)
            {
                return Result<string>.Failure("Ürün adı daha önce kullanılmış");
            }
        }

        mapper.Map(request, product);

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("products");

        return "Ürün kaydı başarıyla güncellendi";
    }
}
