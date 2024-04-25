using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eMuhasebeServer.Domain.Events;

public sealed class SendConfirmEmail(
    UserManager<AppUser> userManager) : INotificationHandler<AppUserEvent>
{
    public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(notification.UserId.ToString());
        if(appUser is not null)
        {
            //Onay maili gönder
        }
    }
}
