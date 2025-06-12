using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Controllers;
using LexiconGruppProject1_grupp7.Web.Views.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.web.Tests;

public class AccountControllerTests
{
    [Fact]
    public async Task Login_Post_Should_Call_SignIn_And_Redirect()
    {
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new UserResultDto());
        var controller = new AccountController(mockService.Object);

        var loginVM = new LoginVM
        {
            UserName = "testuser",
            Password = "password123"
        };

        var result = await controller.Login(loginVM);

        mockService.Verify(s => s.SignInAsync(loginVM.UserName, loginVM.Password), Times.Once);

        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task LogOut_CallsSignOutAndRedirects()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var controller = new AccountController(mockService.Object);

        // Act
        var result = await controller.LogOut();

        // Assert
        mockService.Verify(s => s.SignOutAsync(), Times.Once);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Stories", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Register_Post_InvalidModel_ReturnsView()
    {
        var userService = new Mock<IUserService>();
        var controller = new AccountController(userService.Object);
        controller.ModelState.AddModelError("UserName", "Required");

        var result = await controller.Register(new RegisterVM());

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Register_Post_ValidModel_UserCreationFails_ReturnsViewWithError()
    {
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileDto>(), It.IsAny<string>(), false))
            .ReturnsAsync(new UserResultDto("error"));

        var controller = new AccountController(userService.Object);
        var vm = new RegisterVM
        {
            UserName = "test",
            Email = "test@test.com",
            Password = "pass",
            PasswordRepeat = "pass"
        };

        var result = await controller.Register(vm);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(controller.ModelState.IsValid);
    }

    [Fact]
    public async Task Register_Post_ValidModel_UserCreationSucceeds_RedirectsToUserPage()
    {
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileDto>(), It.IsAny<string>(), false))
            .ReturnsAsync(new UserResultDto());
        userService.Setup(x => x.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new UserResultDto());

        var controller = new AccountController(userService.Object);
        var vm = new RegisterVM
        {
            UserName = "test",
            Email = "test@test.com",
            Password = "pass",
            PasswordRepeat = "pass"
        };

        var result = await controller.Register(vm);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("UserPage", redirect.ActionName);
    }

    [Fact]
    public async Task UserPage_ReturnsViewWithUserData()
    {
        var userService = new Mock<IUserService>();
        var userId = "user123";
        var userProfile = new UserProfileDto("test@test.com", "testuser", "bio");
        userService.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(userProfile);

        var controller = new AccountController(userService.Object);
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, "testuser")
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var result = await controller.UserPage();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<UserPageVM>(viewResult.Model);
        Assert.Equal("testuser", model.UserName);
        Assert.Equal("test@test.com", model.Email);
        Assert.Equal("bio", model.Bio);
    }

    [Fact]
    public async Task AdminPage_ReturnsViewWithUsers()
    {
        var userService = new Mock<IUserService>();
        var users = new[]
        {
            new AdminViewbleUserProfileDto("1", "admin", "admin@test.com", true),
            new AdminViewbleUserProfileDto("2", "user", "user@test.com", false)
        };
        userService.Setup(x => x.AdminGetAllUsers()).ReturnsAsync(users);

        var controller = new AccountController(userService.Object);

        var result = await controller.AdminPage();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<AdminPageVM>(viewResult.Model);
        Assert.Equal(2, model.userVMs.Length);
        Assert.Equal("admin", model.userVMs[0].UserName);
        Assert.True(model.userVMs[0].IsAdmin);
    }
}
