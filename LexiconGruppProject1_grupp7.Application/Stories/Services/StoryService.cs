using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Stories.Services
{
    public class StoryService(IUnitOfWork unitOfWork) : IStoryService
    {
        public async Task AddStoryAsync(Story story)
        {
            await unitOfWork.StoryRepository.AddAsync(story);
            await unitOfWork.PersistAsync();
        }

        public async Task<Story[]> GetAllStoriesAsync()
        {
            return await unitOfWork.StoryRepository.GetAllAsync();
        }
        public async Task<Story> GetStoryByIdAsync(int id)
        {
            return await unitOfWork.StoryRepository.GetByIdAsync(id);
        }
    }
}
