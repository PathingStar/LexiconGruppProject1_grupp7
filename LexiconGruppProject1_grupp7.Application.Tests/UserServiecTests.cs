using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LexiconGruppProject1_grupp7.Application.Tests
{
    public class UserServiecTests
    {
        [Fact]
        public async Task CreateUserAsync_ShouldCallIdentityService_WithCorrectParameters()
        {
            var identityUserService = new Mock<IIdentityUserService>();
            var service=new UserService(identityUserService.Object);
            identityUserService.Setup(o => o.CreateUserAsync(
                It.IsAny<UserProfileDto>(),
                It.IsAny<string>()))
                .ReturnsAsync(new UserResultDto());

            var response = await service.CreateUserAsync(new UserProfileDto("test@test.com", "testsson", null), "password");
            identityUserService.Verify(o => o.CreateUserAsync(
                It.Is<UserProfileDto>(x =>
                    x.Email == "test@test.com" &&
                    x.UserName == "testsson"),
                It.Is<string>(
                    x=> x == "password")
                ), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<UserResultDto>(response);
            Assert.True(response.Succeeded);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUserProfile_WhenUserExists()
        {
            var identityUserService = new Mock<IIdentityUserService>();
            var service = new UserService(identityUserService.Object);

            var profile = new UserProfileDto("test@email", "testUserName");
            identityUserService.Setup(o => o.GetUserByIdAsync("testId")).ReturnsAsync(profile);

            var result = await service.GetUserByIdAsync("testId");

            identityUserService.Verify(o => o.GetUserByIdAsync("testId"), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("test@email", result.Email);
            Assert.Equal("testUserName", result.UserName);
            Assert.IsType<UserProfileDto>(result);
        }

        [Fact]
        public async Task SignInAsync_ShouldReturnUserResultDto_WhenCredentialsAreValid()
        {
            var identityUserService = new Mock<IIdentityUserService>();
            var service = new UserService(identityUserService.Object);

            var resultDto = new UserResultDto();
            identityUserService.Setup(o => o.SignInAsync("testUserName", "testPassword")).ReturnsAsync(resultDto);

            var result = await service.SignInAsync("testUserName", "testPassword");

            identityUserService.Verify(o => o.SignInAsync("testUserName", "testPassword"), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<UserResultDto>(result);
        }

    }
    }
