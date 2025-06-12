using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Controllers;
using LexiconGruppProject1_grupp7.Web.Views.Stories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
namespace LexiconGruppProject1_grupp7.web.Tests
{
    public class StoryControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WithExpectedViewModel()
        {
            // Arrange
            var mockService = new Mock<IStoryService>();
             mockService.Setup(s => s.GetAllStoriesAsync()).Returns(Task.FromResult( new Story[]
        {
            new Story { Id = 1, Title = "Story 1", Content = "Content 1" },
            new Story { Id = 2, Title = "Story 2", Content = "Content 2" }

        }
        ));

            var controller = new StoriesController(mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<IndexVM>(viewResult.Model);

            Assert.Equal(2, model.StoryItems.Length);
            Assert.Contains(model.StoryItems, s => s.StoryId == 1 && s.StoryTitle == "Story 1");
            Assert.Contains(model.StoryItems, s => s.StoryId == 2 && s.StoryTitle == "Story 2");
        }

        [Fact]
        public async Task Details_ReturnsViewWithCorrectModel_WhenStoryExists()
        {
            var mockService = new Mock<IStoryService>();
            mockService.Setup(s => s.GetStoryByIdAsync(1)).Returns(Task.FromResult(
                new Story { Title = "Story 1", Content = "Content 1" }
            ));
            var controller = new StoriesController(mockService.Object);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DetailsVM>(viewResult.Model);

            Assert.Equal("Story 1", model.StoryTitle);
            Assert.Equal("Content 1", model.StoryContent);
        }

        [Fact]
        public async Task Create_CallsAddStoryAsync_WithCorrectStoryData()
        {
            var mockService = new Mock<IStoryService>();
            mockService.Setup(s => s.AddStoryAsync(It.IsAny<Story>()));
            var controller = new StoriesController(mockService.Object);

            await controller.Create(new CreateVM
            {
                StoryTitle = "Story 1",
                StoryContent = "Content 1"
            });

            mockService.Verify(o => o.AddStoryAsync(
                It.Is<Story>(x=>
                x.Title == "Story 1" &&
                x.Content == "Content 1")), Times.Once);
        }
    }
}
