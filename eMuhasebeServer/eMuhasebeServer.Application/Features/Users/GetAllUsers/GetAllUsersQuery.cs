using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.GetAllUsers;
public sealed record GetAllUsersQuery() : IRequest<Result<List<AppUser>>>;
