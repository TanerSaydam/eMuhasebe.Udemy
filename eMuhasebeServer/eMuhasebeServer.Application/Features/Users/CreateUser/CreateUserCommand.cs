using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUser;
public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    List<Guid> CompanyIds,
    bool IsAdmin) : IRequest<Result<string>>;
