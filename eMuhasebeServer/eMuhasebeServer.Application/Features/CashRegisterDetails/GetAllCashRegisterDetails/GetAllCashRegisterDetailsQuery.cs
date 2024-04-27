using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails;
public sealed record GetAllCashRegisterDetailsQuery(
    Guid CashRegisterId,
    DateOnly StartDate,
    DateOnly EndDate): IRequest<Result<CashRegister>>;
