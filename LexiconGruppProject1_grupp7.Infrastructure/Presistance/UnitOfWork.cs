using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Infrastructure.Presistance
{
    public class UnitOfWork(ApplicationContext context, IStoryRepository storyRepository) : IUnitOfWork
    {
        public IStoryRepository StoryRepository { get; } = storyRepository;
        public async Task PersistAsync()
        {
            await context.SaveChangesAsync();
        }
    }
    
    
}
