using LexiconGruppProject1_grupp7.Domain.Entities;

namespace LexiconGruppProject1_grupp7.Application.Stories.Interfaces
{
    public interface IStoryRepository
    {
        Task AddAsync(Story story);
        Task<Story[]> GetAllAsync();
        Task<Story> GetByIdAsync(int id);
    }
}