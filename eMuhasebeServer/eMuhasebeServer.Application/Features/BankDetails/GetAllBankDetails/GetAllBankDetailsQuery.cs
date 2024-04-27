using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.GetAllBankDetails;
public sealed record GetAllBankDetailsQuery(
    Guid BankId,
    DateOnly StartDate,
    DateOnly EndDate) : IRequest<Result<Bank>>;
