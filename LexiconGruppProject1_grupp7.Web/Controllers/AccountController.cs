using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace LexiconGruppProject1_grupp7.Web.Controllers;
public class AccountController(IUserService userService) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if(loginVM.UserName == null || loginVM.Password == null)
        {
            ModelState.AddModelError(string.Empty, "Username and password cannot be empty.");
            return View();
        }
       


        var result = await userService.SignInAsync(loginVM.UserName, loginVM.Password);
        if (!result.Succeeded)
        {
            return View();
        }
        return RedirectToAction(nameof(UserPage));
    }


    [Route("logout")]
    public async Task<IActionResult> LogOut()
    {
        await userService.SignOutAsync();


        return RedirectToAction(nameof(StoriesController.Index), nameof(StoriesController).Replace("Controller", String.Empty));
    }


    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if(registerVM.Password!= registerVM.PasswordRepeat)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match");
            return View();
        }
        var result = await userService.CreateUserAsync(new UserProfileDto(registerVM.Email, registerVM.UserName), registerVM.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }
        await userService.SignInAsync(registerVM.UserName, registerVM.Password);

        return RedirectToAction(nameof(UserPage));
    }
    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> UserPage()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userProfile = await userService.GetUserByIdAsync(userId);
        var viewModel = new UserPageVM
        {
            UserName = User.Identity.Name,
            Bio = userProfile.Bio,
            Email = userProfile.Email
        };

        return View(viewModel);
    }
    [Authorize]
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<IActionResult> AdminPage()
    {
        var users = await userService.AdminGetAllUsers();
        AdminPageVM adminPageVM = new AdminPageVM
        {
            userVMs = users.Select(u => new AdminPageVM.UserVM
            {
                Id = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                IsAdmin = u.isAdmin
            }).ToArray()
        };
        return View(adminPageVM);
    }

    [HttpPost("admin")]
    public async Task<IActionResult> AdminPage(AdminPageVM adminPageVM)
    {

        return RedirectToAction(nameof(AdminPage));

    }
    [HttpPost("admin/toggleadmin")]
    public async Task<IActionResult> ToggleAdmin(string userId, bool isAdmin)
    {

        if (isAdmin)
        {

            await userService.AddRoleAsync(userId, "Admin");
        }
        else
        {
            await userService.RemoveRoleAsync(userId, "Admin");

        }

        return RedirectToAction(nameof(AdminPage));
    }

}
