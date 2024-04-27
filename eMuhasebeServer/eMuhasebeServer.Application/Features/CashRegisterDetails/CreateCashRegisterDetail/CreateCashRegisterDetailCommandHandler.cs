using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;

internal sealed class CreateCashRegisterDetailCommandHandler(
    ICashRegisterRepository cashRegisterRepository
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
    ) : IRequestHandler<CreateCashRegisterDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCashRegisterDetailCommand request, CancellationToken cancellationToken)
    {
        CashRegister cashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(p=> p.Id == request.CashRegisterId, cancellationToken);

        cashRegister.DepositAmount += (request.Type == 0 ? request.Amount : 0);
        cashRegister.WithdrawalAmount += (request.Type == 1 ? request.Amount : 0);

        CashRegisterDetail cashRegisterDetail = new()
        {
            Date = request.Date,
            DepositAmount = request.Type == 0 ? request.Amount : 0,
            WithdrawalAmount = request.Type == 1 ? request.Amount : 0,
            CashRegisterDetailId = request.CashRegisterDetailId,
            Description = request.Description,
            CashRegisterId = request.CashRegisterId
        };

        await cashRegisterDetailRepository.AddAsync(cashRegisterDetail, cancellationToken);

        if(request.CashRegisterDetailId is not null)
        {
            CashRegister oppositeCashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.CashRegisterDetailId, cancellationToken);

            cashRegister.DepositAmount += (request.Type == 1 ? request.OppositeAmount : 0);
            cashRegister.WithdrawalAmount += (request.Type == 0 ? request.OppositeAmount : 0);

            CashRegisterDetail oppositeCashRegisterDetail = new()
            {
                Date = request.Date,
                DepositAmount = request.Type == 1 ? request.OppositeAmount : 0,
                WithdrawalAmount = request.Type == 0 ? request.OppositeAmount : 0,
                CashRegisterDetailId = cashRegisterDetail.Id,
                Description = request.Description,
                CashRegisterId = (Guid)request.CashRegisterDetailId
            };

            await cashRegisterDetailRepository.AddAsync(oppositeCashRegisterDetail, cancellationToken);
        }

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("cashRegisters");

        return "Kasa hareketi başarıyla işlendi";

    }
}
