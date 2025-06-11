using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Infrastructure.Services;

public class IdentityUserSevice(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : IIdentityUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password, bool isAdmin)
    {
        var result = await userManager.CreateAsync(new ApplicationUser
        {
            UserName = user.UserName,
            Email = user.Email,

        }, password);

        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserProfileDto> GetUserByIdAsync(string userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);
        return new UserProfileDto(applicationUser.Email, applicationUser.UserName, applicationUser.Bio);
    }

    public async Task<UserResultDto> SignInAsync(string userName, string password)
    {
        var result = await signInManager.PasswordSignInAsync(userName, password, false, false);
        return new UserResultDto(result.Succeeded ? null : "Invalid user credentials");
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
