using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;
internal sealed class ProductRepository : Repository<Product, CompanyDbContext>, IProductRepository
{
    public ProductRepository(CompanyDbContext context) : base(context)
    {
    }
}
