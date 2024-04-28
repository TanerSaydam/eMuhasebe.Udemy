using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CustomerDetails.GetAllCustomerDetails;

internal sealed class GetAllCustomerDetailsQueryHandler(
    ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomerDetailsQuery, Result<Customer>>
{
    public async Task<Result<Customer>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = 
            await customerRepository
            .Where(p => p.Id == request.CustomerId)
            .Include(p=> p.Details)
            .FirstOrDefaultAsync(cancellationToken);

        if(customer is null)
        {
            return Result<Customer>.Failure("Cari bulunamadı");
        }

        return customer;
    }
}
