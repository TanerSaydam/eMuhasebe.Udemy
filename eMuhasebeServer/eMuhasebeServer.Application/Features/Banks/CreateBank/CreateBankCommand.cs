using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.CreateBank;
public sealed record CreateBankCommand(
    string Name,
    string IBAN,
    int CurrencyTypeValue) : IRequest<Result<string>>;
