using LexiconGruppProject1_grupp7.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
public interface IIdentityUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password, bool isAdmin);
    Task<UserResultDto> SignInAsync(string userName, string password);
}
