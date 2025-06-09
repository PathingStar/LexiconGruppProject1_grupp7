using LexiconGruppProject1_grupp7.Domain.Entities;

namespace LexiconGruppProject1_grupp7.Application.Stories.Interfaces
{
    public interface IStoryService
    {
        Task AddStoryAsync(Story story);
        Task<Story[]> GetAllStoriesAsync();
        Task<Story> GetStoryByIdAsync(int id);
    }
}