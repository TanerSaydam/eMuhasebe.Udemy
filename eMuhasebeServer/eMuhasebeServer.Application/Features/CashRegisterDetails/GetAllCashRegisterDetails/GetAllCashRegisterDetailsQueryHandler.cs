using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails;

internal sealed class GetAllCashRegisterDetailsQueryHandler(
    ICashRegisterRepository cashRegisterRepository) : IRequestHandler<GetAllCashRegisterDetailsQuery, Result<CashRegister>>
{
    public async Task<Result<CashRegister>> Handle(GetAllCashRegisterDetailsQuery request, CancellationToken cancellationToken)
    {
        CashRegister? cashRegister =
            await cashRegisterRepository
            .Where(p => p.Id == request.CashRegisterId)
            .Include(p => p.CashRegisterDetails!.Where(p => p.Date >= request.StartDate && p.Date <= request.EndDate))
            .FirstOrDefaultAsync(cancellationToken);

        if(cashRegister is null)
        {
            return Result<CashRegister>.Failure("Kasa bulunamadı");
        }

        return cashRegister;
    }
}
