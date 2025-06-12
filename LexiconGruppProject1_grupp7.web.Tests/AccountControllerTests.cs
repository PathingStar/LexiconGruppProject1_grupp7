using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Controllers;
using LexiconGruppProject1_grupp7.Web.Views.Account;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
