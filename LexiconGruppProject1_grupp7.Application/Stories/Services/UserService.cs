using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Stories.Services;
public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password, bool isAdmin)
    {
        return await identityUserService.CreateUserAsync(user, password, isAdmin);
    }

    public async Task<UserProfileDto> GetUserByIdAsync(string userId)
    {

        return await identityUserService.GetUserByIdAsync(userId);
    }

    public async Task<UserResultDto> SignInAsync(string userName, string password)
    {
        return await identityUserService.SignInAsync(userName, password);
    }

    public async Task SignOutAsync()
    {
        await identityUserService.SignOutAsync();
    }
    public async Task<AdminViewbleUserProfileDto[]> AdminGetAllUsers()    {
        return await identityUserService.AdminGetAllUsers();
    }
}
