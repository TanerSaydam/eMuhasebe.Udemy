using eMuhasebeServer.Application.Features.Auth.Login;
using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}
