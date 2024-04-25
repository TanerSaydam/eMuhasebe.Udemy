using eMuhasebeServer.Domain.ValueObjects;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.UpdateCompany;
public sealed record UpdateCompanyCommand(
    Guid Id,
    string Name,
    string FullAddress,
    string TaxDepartment,
    string TaxNumber,
    Database Database) : IRequest<Result<string>>;
