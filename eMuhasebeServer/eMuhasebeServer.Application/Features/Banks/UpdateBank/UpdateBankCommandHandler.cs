using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.UpdateBank;

internal sealed class UpdateBankCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateBankCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        Bank? bank = await bankRepository.GetByExpressionWithTrackingAsync(p=> p.Id == request.Id, cancellationToken);

        if(bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }

        if(bank.IBAN != request.IBAN)
        {
            bool isIBANExists = await bankRepository.AnyAsync(p => p.IBAN == request.IBAN, cancellationToken);
            if (isIBANExists)
            {
                return Result<string>.Failure("IBAN daha önce kaydedilmiş");
            }
        }

        mapper.Map(request, bank);

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");

        return "Banka bilgileri başarıyla güncellendi";
    }
}
