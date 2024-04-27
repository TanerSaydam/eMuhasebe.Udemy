using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisters.UpdateCashRegister;

internal sealed class UpdateCashRegisterCommandHandler(
    ICashRegisterRepository cashRegisterRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateCashRegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCashRegisterCommand request, CancellationToken cancellationToken)
    {
        CashRegister? cashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if(cashRegister is null)
        {
            return Result<string>.Failure("Kasa kaydı bulunamadı");
        }

        if(cashRegister.Name != request.Name)
        {
            bool isNameExists = await cashRegisterRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isNameExists)
            {
                return Result<string>.Failure("Bu kasa adı daha önce kullanılmış");
            }
        }

        mapper.Map(request, cashRegister);

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("cashRegisters");

        return "Kasa kaydı başarıyla güncellendi";
    }
}
