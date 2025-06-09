using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Application.Stories.Services
{
    public class StoryService(IStoryRepository storyRepository) : IStoryService
    {
        public async Task AddStoryAsync(Story story)
        {
            await storyRepository.AddAsync(story);
        }

        public async Task<Story[]> GetAllStoriesAsync()
        {
            return await storyRepository.GetAllAsync();
        }
        public async Task<Story> GetStoryByIdAsync(int id)
        {
            return await storyRepository.GetByIdAsync(id);
        }
    }
}
