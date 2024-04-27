using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.BankDetails.DeleteBankDetailById;
public sealed record DeleteBankDetailByIdCommand(
    Guid Id) : IRequest<Result<string>>;
