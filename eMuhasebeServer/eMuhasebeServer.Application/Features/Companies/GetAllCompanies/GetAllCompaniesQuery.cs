using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.GetAllCompanies;
public sealed record GetAllCompaniesQuery() : IRequest<Result<List<Company>>>;
