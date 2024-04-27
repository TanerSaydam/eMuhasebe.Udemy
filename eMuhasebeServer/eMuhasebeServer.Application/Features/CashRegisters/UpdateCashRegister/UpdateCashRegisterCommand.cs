using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisters.UpdateCashRegister;
public sealed record UpdateCashRegisterCommand(
    Guid Id,
    string Name,
    int TypeValue) : IRequest<Result<string>>;
