using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = mapper.Map<Customer>(request);

        await customerRepository.AddAsync(customer, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("customers");

        return "Cari kaydı başarıyla tamamlandı";
    }
}
