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
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager)
    : IIdentityUserService
{
    public async Task AddRoleAsync(ApplicationUser user, string roleName)
    {
        // Skapa en ny roll
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));

        // Lägg till en användare till en roll
        //if (addUserToRole)
            await userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
    {
        var newApplicationUser = new ApplicationUser
        {
            UserName = user.UserName,
            Email = user.Email,

        };
        var result = await userManager.CreateAsync(newApplicationUser, password);
        //if (isAdmin)
        //    await AddRoleAsync(newApplicationUser, "Admin");
        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserProfileDto> GetUserByIdAsync(string userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);
        return new UserProfileDto(applicationUser.Email, applicationUser.UserName, applicationUser.Bio);
    }
    public async Task<AdminViewbleUserProfileDto[]> AdminGetAllUsers() 
    {
        var users =  userManager.Users.ToArray();
        return users.Select(u => new AdminViewbleUserProfileDto(
        
            u.Id,
            u.UserName,
            u.Email,
            userManager.IsInRoleAsync(u, "Admin").Result
            )
            
        ).ToArray();


        //bool isUserInRole = await userManager.IsInRoleAsync(user, RoleName);

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
