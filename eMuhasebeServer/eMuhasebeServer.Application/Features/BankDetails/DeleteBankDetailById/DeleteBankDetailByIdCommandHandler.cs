using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.DeleteBankDetailById;

internal sealed class DeleteBankDetailByIdCommandHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IBankRepository bankRepository,
    IBankDetailRepository bankDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService) : IRequestHandler<DeleteBankDetailByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBankDetailByIdCommand request, CancellationToken cancellationToken)
    {
        BankDetail? bankDetail =
            await bankDetailRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (bankDetail is null)
        {
            return Result<string>.Failure("Banka hareketi bulunamadı");
        }

        Bank? bank =
            await bankRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == bankDetail.BankId, cancellationToken);

        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }

        bank.DepositAmount -= bankDetail.DepositAmount;
        bank.WithdrawalAmount -= bankDetail.WithdrawalAmount;

        if (bankDetail.BankDetailId is not null)
        {
            BankDetail? oppositeBankDetail =
            await bankDetailRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == bankDetail.BankDetailId, cancellationToken);

            if (oppositeBankDetail is null)
            {
                return Result<string>.Failure("Banka hareketi bulunamadı");
            }

            Bank? oppositeBank =
            await bankRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == oppositeBankDetail.BankId, cancellationToken);

            if (oppositeBank is null)
            {
                return Result<string>.Failure("Banka bulunamadı");
            }

            oppositeBank.DepositAmount -= oppositeBankDetail.DepositAmount;
            oppositeBank.WithdrawalAmount -= oppositeBankDetail.WithdrawalAmount;

            bankDetailRepository.Delete(oppositeBankDetail);
        }

        if (bankDetail.CashRegisterDetailId is not null)
        {
            CashRegisterDetail? oppositeCashRegisterDetail =
            await cashRegisterDetailRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == bankDetail.CashRegisterDetailId, cancellationToken);

            if (oppositeCashRegisterDetail is null)
            {
                return Result<string>.Failure("Kasa hareketi bulunamadı");
            }

            CashRegister? oppositeCashRegister =
            await cashRegisterRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == oppositeCashRegisterDetail.CashRegisterId, cancellationToken);

            if (oppositeCashRegister is null)
            {
                return Result<string>.Failure("Kasa bulunamadı");
            }

            oppositeCashRegister.DepositAmount -= oppositeCashRegisterDetail.DepositAmount;
            oppositeCashRegister.WithdrawalAmount -= oppositeCashRegisterDetail.WithdrawalAmount;

            cashRegisterDetailRepository.Delete(oppositeCashRegisterDetail);
        }

        bankDetailRepository.Delete(bankDetail);

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");
        cacheService.Remove("cashRegisters");

        return "Banka hareketi başarıyla silindi";
    }
}
