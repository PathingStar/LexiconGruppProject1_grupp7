using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;

namespace LexiconGruppProject1_grupp7.Application.Stories.Interfaces
{
    public interface IUnitOfWork
    {
        IStoryRepository StoryRepository { get; }

        Task PersistAsync();
    }
}