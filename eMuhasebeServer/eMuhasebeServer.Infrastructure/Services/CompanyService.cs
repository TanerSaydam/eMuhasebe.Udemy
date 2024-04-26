using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeServer.Infrastructure.Services;
internal sealed class CompanyService : ICompanyService
{
    public void MigrateAll(List<Company> companies)
    {
        foreach (var company in companies)
        {
            CompanyDbContext context = new(company);

            context.Database.Migrate();
        }
    }
}
