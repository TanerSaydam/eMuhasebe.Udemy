using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IMediator mediator,
    UserManager<AppUser> userManager,
    ICompanyUserRepository companyUserRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = 
            await userManager.Users
            .Where(p=> p.Id == request.Id)
            .Include(p=>p.CompanyUsers)
            .FirstOrDefaultAsync(cancellationToken);

        bool isMailChanged = false;

        if(appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        if(appUser.UserName != request.UserName)
        {
            bool isUserNameExists = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);

            if (isUserNameExists)
            {
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış");
            }
        }

        if(appUser.Email != request.Email)
        {
            bool isEmailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);

            if (isEmailExists)
            {
                return Result<string>.Failure("Bu mail adresi daha önce kullanılmış");
            }

            isMailChanged = true;
            appUser.EmailConfirmed = false;
        }

        mapper.Map(request, appUser);

        IdentityResult identityResult = await userManager.UpdateAsync(appUser);


        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }

        if(request.Password is not null)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(appUser);

            identityResult = await userManager.ResetPasswordAsync(appUser, token, request.Password);

            if (!identityResult.Succeeded)
            {
                return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
            }
        }

        companyUserRepository.DeleteRange(appUser.CompanyUsers);

        List<CompanyUser> companyUsers = request.CompanyIds.Select(s => new CompanyUser
        {
            AppUserId = appUser.Id,
            CompanyId = s
        }).ToList();

        await companyUserRepository.AddRangeAsync(companyUsers, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (isMailChanged)
        {
            await mediator.Publish(new AppUserEvent(appUser.Id));
        }

        return "Kullanıcı başarıyla güncellendi";
    }
}
