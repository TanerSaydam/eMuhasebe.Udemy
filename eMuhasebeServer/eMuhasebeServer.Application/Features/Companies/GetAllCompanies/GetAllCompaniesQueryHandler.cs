using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.GetAllCompanies;

internal sealed class GetAllCompaniesQueryHandler(
    ICompanyRepository companyRepository,
    ICacheService cacheService) : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
{
    public async Task<Result<List<Company>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        List<Company>? companies;

        companies = cacheService.Get<List<Company>>("companies");
        
        if(companies is null)
        {
            companies =
            await companyRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

            cacheService.Set<List<Company>>("companies", companies);
        }        

        return companies;
    }
}
