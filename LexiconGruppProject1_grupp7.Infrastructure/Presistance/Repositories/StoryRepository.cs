using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Infrastructure.Presistance.Repositories
{
    public class StoryRepository(ApplicationContext context) : IStoryRepository
    {
        public async Task<Story[]> GetAllAsync() => await context.Stories.ToArrayAsync();
    }
}
