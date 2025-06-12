using System.Collections.Generic;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Controllers;
using LexiconGruppProject1_grupp7.Web.Views.Stories;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
    }
}
