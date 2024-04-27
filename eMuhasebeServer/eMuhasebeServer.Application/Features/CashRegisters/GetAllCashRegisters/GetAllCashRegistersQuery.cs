using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CashRegisters.GetAllCashRegisters;
public sealed record GetAllCashRegistersQuery() : IRequest<Result<List<CashRegister>>>;
