using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.DeleteBankById;

internal sealed class DeleteBankByIdCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService) : IRequestHandler<DeleteBankByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBankByIdCommand request, CancellationToken cancellationToken)
    {
        Bank? bank = await bankRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }

        bank.IsDeleted = true;

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");

        return "Banka başarıyla silindi";
    }
}
