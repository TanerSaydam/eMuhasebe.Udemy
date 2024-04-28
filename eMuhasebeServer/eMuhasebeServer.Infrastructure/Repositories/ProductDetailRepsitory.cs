using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;
internal sealed class ProductDetailRepsitory : Repository<ProductDetail, CompanyDbContext>, IProductDetailRepository
{
    public ProductDetailRepsitory(CompanyDbContext context) : base(context)
    {
    }
}
