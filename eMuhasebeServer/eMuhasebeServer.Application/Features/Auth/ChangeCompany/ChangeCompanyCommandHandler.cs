using eMuhasebeServer.Application.Features.Auth.Login;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Auth.ChangeCompany;

internal sealed class ChangeCompanyCommandHandler(
    ICompanyUserRepository companyUserRepository,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IJwtProvider jwtProvider) : IRequestHandler<ChangeCompanyCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(ChangeCompanyCommand request, CancellationToken cancellationToken)
    {
        if(httpContextAccessor.HttpContext is null)
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok");
        }

        string? userIdString = httpContextAccessor.HttpContext.User.FindFirstValue("Id");

        if (string.IsNullOrEmpty(userIdString))
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok");
        }

        AppUser? appUser = await userManager.FindByIdAsync(userIdString);
        if(appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı");
        }

        List<CompanyUser> companyUsers = await companyUserRepository.Where(p => p.AppUserId == appUser.Id).Include(p => p.Company).ToListAsync(cancellationToken);

        List<Company> companies = companyUsers.Select(s => new Company
        {
            Id = s.CompanyId,
            Name = s.Company!.Name,
            TaxDepartment = s.Company!.TaxDepartment,
            TaxNumber = s.Company!.TaxNumber,
            FullAddress = s.Company!.FullAddress,
        }).ToList();

        var response = await jwtProvider.CreateToken(appUser, request.CompanyId, companies);

        return response;
    }
}
