using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;
public sealed record CreateCashRegisterDetailCommand(
    Guid CashRegisterId,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? OppositeCashRegisterId,
    Guid? OppositeBankId,
    decimal OppositeAmount,
    string Description
    ) : IRequest<Result<string>>;
