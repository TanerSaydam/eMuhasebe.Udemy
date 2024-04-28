using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.CreateBankDetail;
public sealed record CreateBankDetailCommand(
    Guid BankId,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? OppositeBankId,
    Guid? OppositeCashRegisterId,
    decimal OppositeAmount,
    string Description) : IRequest<Result<string>>;
