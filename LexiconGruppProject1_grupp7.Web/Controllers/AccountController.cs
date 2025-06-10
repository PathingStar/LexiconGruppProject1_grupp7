using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiconGruppProject1_grupp7.Web.Controllers;
public class AccountController(IUserService userService) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string userName, string password)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await userService.SignInAsync(userName, password);
        if (!result.Succeeded)
        {
            return View();
        }
        return RedirectToAction(nameof(User));
    }


    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string userName, string email, string password)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var result = await userService.CreateUserAsync(new UserProfileDto(email, userName), password, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }
        await userService.SignInAsync(userName, password);

        return RedirectToAction(nameof(User));
    }

    [HttpGet("user")]
    public async Task<IActionResult> User()
    {

        return View();
    }
}
