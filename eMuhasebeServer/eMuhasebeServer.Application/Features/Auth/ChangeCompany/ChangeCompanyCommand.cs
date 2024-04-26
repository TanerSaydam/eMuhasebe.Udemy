using eMuhasebeServer.Application.Features.Auth.Login;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Auth.ChangeCompany;
public sealed record ChangeCompanyCommand(Guid CompanyId) : IRequest<Result<LoginCommandResponse>>;
