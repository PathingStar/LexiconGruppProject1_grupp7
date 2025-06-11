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
        

    }
}
