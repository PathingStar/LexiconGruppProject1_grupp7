using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Controllers;
using LexiconGruppProject1_grupp7.Web.Views.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.web.Tests
{
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
        public async Task Login_Post_Should_Return_ViewResult_When_ModelState_Is_Invalid()
        {
            var mockService = new Mock<IUserService>();
            var controller = new AccountController(mockService.Object);
            controller.ModelState.AddModelError("UserName", "Required");
            var loginVM = new LoginVM
            {
                UserName = "testuser",
                Password = "password123"
            };
            var result = await controller.Login(loginVM);
            Assert.IsType<ViewResult>(result);


        }
        [Fact]
        public async Task LogOut_Should_Call_SignOut_And_Redirect()
        {
            var mockService = new Mock<IUserService>();
            var controller = new AccountController(mockService.Object);
            var result = await controller.LogOut();
            mockService.Verify(s => s.SignOutAsync(), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }


        [Fact]
        public async Task Register_Post_Should_Call_CreateUser_And_Redirect()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.CreateUserAsync(It.IsAny<UserProfileDto>(), It.IsAny<string>(), false))
                .ReturnsAsync(new UserResultDto());
            var controller = new AccountController(mockService.Object);
            var registerVM = new RegisterVM
            {
                UserName = "newuser",
                Email = "test@mail",
                Password = "password123",
                PasswordRepeat = "password123"

            };
            var result = await controller.Register(registerVM);
            mockService.Verify(s => s.CreateUserAsync(
                It.Is<UserProfileDto>(u => u.UserName == registerVM.UserName &&
            u.Email == registerVM.Email), registerVM.Password, false), Times.Once);
        }
        [Fact]
        public async Task Register_Post_Should_Return_ViewResult_When_ModelState_Is_Invalid()
        {
            var mockService = new Mock<IUserService>();
            var controller = new AccountController(mockService.Object);
            controller.ModelState.AddModelError("UserName", "Required");
            var registerVM = new RegisterVM
            {
                UserName = "newuser",
                Email = "test@mail",
                Password = "password123",
                PasswordRepeat = "password123"
            };
            var result = await controller.Register(registerVM);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Register_Post_Should_Return_ViewResult_When_CreateUser_Fails()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.CreateUserAsync(It.IsAny<UserProfileDto>(), It.IsAny<string>(), false))
                .ReturnsAsync(new UserResultDto("Error creating user"));
            var controller = new AccountController(mockService.Object);
            var registerVM = new RegisterVM
            {
                UserName = "newuser",
                Email = "test@mail",
                Password = "password123",
                PasswordRepeat = "password123"
            };
            var result = await controller.Register(registerVM);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.Contains("Error creating user", viewResult.ViewData.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }
        [Fact]
        public async Task Register_Post_Should_Return_ViewResult_When_Passwords_Do_Not_Match()
        {
            var mockService = new Mock<IUserService>();
            var controller = new AccountController(mockService.Object);
            var registerVM = new RegisterVM
            {
                UserName = "newuser",
                Email = "test@mail",
                Password = "password123",
                PasswordRepeat = "differentpassword"
            };
            var result = await controller.Register(registerVM);
            Assert.IsType<ViewResult>(result);
            Assert.Contains("Passwords do not match", controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }
        [Fact]
        public async Task UserPage_Should_Return_ViewResult()
        {
            var mockService = new Mock<IUserService>();
            var controller = new AccountController(mockService.Object);

            var user = new ClaimsPrincipal(
                
                new ClaimsIdentity(
                    new Claim[]

                {       

                    new Claim(ClaimTypes.NameIdentifier, "123"),

                    new Claim(ClaimTypes.Name, "testuser")

                }, "TestAuth"));

            

            controller.ControllerContext = new ControllerContext

            {

                HttpContext = new DefaultHttpContext { User = user }

            };


            mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserProfileDto("email@mail", "username", "bio"));
            var result = await controller.UserPage();
            Assert.IsType<ViewResult>(result);
            mockService.Verify(s => s.GetUserByIdAsync(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task AdminGetAllUsers_Should_Return_ViewResult_With_Users()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.AdminGetAllUsers()).ReturnsAsync(new AdminViewbleUserProfileDto[]
            {
                new AdminViewbleUserProfileDto("1", "user1", "user1@mail", false),
                new AdminViewbleUserProfileDto("2", "user2", "user2@mail", true)
            });
            var controller = new AccountController(mockService.Object);
            var result = await controller.AdminPage();
            mockService.Verify(s => s.AdminGetAllUsers(), Times.Once);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            var model = viewResult.Model as AdminPageVM;
            Assert.NotNull(model);
            Assert.Equal(2, model.userVMs.Length);
            Assert.Contains(model.userVMs, u => u.Id == "1" && u.UserName == "user1" && u.Email == "user1@mail" && !u.IsAdmin);
            Assert.Contains(model.userVMs, u => u.Id == "2" && u.UserName == "user2" && u.Email == "user2@mail" && u.IsAdmin);

        }
        [Fact]
        public async Task ToggleAdminTest()
        {

        }
    }
}
