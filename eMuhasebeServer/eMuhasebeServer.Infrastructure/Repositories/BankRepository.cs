using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;
internal sealed class BankRepository : Repository<Bank, CompanyDbContext>, IBankRepository
{
    public BankRepository(CompanyDbContext context) : base(context)
    {
    }
}
