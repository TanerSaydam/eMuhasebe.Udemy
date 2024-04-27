using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.GetAllBanks;
public sealed record GetAllBanksQuery(): IRequest<Result<List<Bank>>>;
