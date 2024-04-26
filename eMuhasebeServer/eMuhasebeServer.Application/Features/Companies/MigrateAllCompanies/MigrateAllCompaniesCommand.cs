using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.MigrateAllCompanies;
public sealed record MigrateAllCompaniesCommand() : IRequest<Result<string>>;
