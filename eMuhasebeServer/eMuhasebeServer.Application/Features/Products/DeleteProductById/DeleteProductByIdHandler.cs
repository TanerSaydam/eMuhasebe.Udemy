using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.DeleteProductById;

internal sealed class DeleteProductByIdHandler(
    IProductRepository productRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService) : IRequestHandler<DeleteProductByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        product.IsDeleted = true;

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("products");

        return "Ürün kaydı başarıyla silindi";
    }
}
