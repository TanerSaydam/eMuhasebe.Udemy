using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Customers.DeleteCustomerById;

internal sealed class DeleteCustomerByIdCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService) : IRequestHandler<DeleteCustomerByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {

        Customer? customer = await customerRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (customer is null)
        {
            return Result<string>.Failure("Cari bulunamadı");
        }

        customer.IsDeleted = true;

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("customers");

        return "Cari başarıyla silindi";
    }
}
