using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisters.GetAllCashRegisters;

internal sealed class GetAllCashRegistersQueryHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICacheService cacheService) : IRequestHandler<GetAllCashRegistersQuery, Result<List<CashRegister>>>
{
    public async Task<Result<List<CashRegister>>> Handle(GetAllCashRegistersQuery request, CancellationToken cancellationToken)
    {
        List<CashRegister>? cashRegisters;

        cashRegisters = cacheService.Get<List<CashRegister>>("cashRegisters");

        if(cashRegisters is null)
        {
            cashRegisters =
            await cashRegisterRepository
            .GetAll().OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

            cacheService.Set("cashRegisters", cashRegisters);
        }
        

        return cashRegisters;
    }
}
