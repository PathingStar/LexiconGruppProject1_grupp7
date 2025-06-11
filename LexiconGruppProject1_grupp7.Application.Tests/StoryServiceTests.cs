using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using Moq;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Tests
{
    public class StoryServiceTests
    {
        [Fact]
        public async Task AddStoryAsync_WhenCalled_CallsRepositoryAndPersist()
        {
            Mock<IStoryRepository> repository = new Mock<IStoryRepository>();
            Mock<IUnitOfWork> work = new Mock<IUnitOfWork>();
            work.Setup(o => o.StoryRepository).Returns(repository.Object);
            var storyService = new StoryService(work.Object);
            var story = new Story { Id = 1, Title = "Test Story", Content = "This is a test story." };
            await storyService.AddStoryAsync(story);

            work.Verify(o => o.StoryRepository.AddAsync(It.Is<Story>(x =>
            x.Title == "Test Story" &&
            x.Content == "This is a test story.")), Times.Once);
            work.Verify(o => o.PersistAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllStoriesAsync_ShouldReturnAllStories()
        {
            Mock<IStoryRepository> repository = new Mock<IStoryRepository>();
            Mock<IUnitOfWork> work = new Mock<IUnitOfWork>();
            work.Setup(o => o.StoryRepository).Returns(repository.Object);
            var storyService = new StoryService(work.Object);
            repository.Setup(o => o.GetAllAsync()).ReturnsAsync(new Story[]
            {
                new Story { Id = 1, Title = "Test Story", Content = "This is a test story." },
                new Story { Id = 2, Title = "Another Story", Content = "This is another test story." }
            });
            
            var stories = await storyService.GetAllStoriesAsync();
            Assert.NotNull(stories);
            Assert.Equal(2, stories.Length);
            Assert.Equal("Test Story", stories[0].Title);
            Assert.Equal("This is a test story.", stories[0].Content);
            Assert.Equal("Another Story", stories[1].Title);
            Assert.Equal("This is another test story.", stories[1].Content);
        }

        [Fact]
        public async Task GetStoryByIdAsync_ShouldReturnCorrectStory()
        {
            Mock<IStoryRepository> repository = new Mock<IStoryRepository>();
            Mock<IUnitOfWork> work = new Mock<IUnitOfWork>();
            work.Setup(o => o.StoryRepository).Returns(repository.Object);
            var storyService = new StoryService(work.Object);

            repository.Setup(o => o.GetByIdAsync(1)).ReturnsAsync(new Story { Id = 1, Title = "Test Story", Content = "This is a test story." });
            
            var story = await storyService.GetStoryByIdAsync(1);
            Assert.NotNull(story);
            Assert.Equal(1, story.Id);
            Assert.Equal("Test Story", story.Title);
            Assert.Equal("This is a test story.", story.Content);
        }



    }

       
    }
