using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        return RedirectToAction(nameof(StoriesController.Index));
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
        var result = await userService.CreateUserAsync(new UserProfileDto(registerVM.Email, registerVM.UserName), registerVM.Password, false);

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
}
