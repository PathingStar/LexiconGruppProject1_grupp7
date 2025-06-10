using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance.Repositories;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;

namespace LexiconGruppProject1_grupp7.Terminal
{
    internal class Program
    {
        static StoryService StoryService { get; set; }
        static async Task Main()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var app = builder.Build();
            string connectionsSettings = app.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(connectionsSettings).Options;
            var context = new ApplicationContext(options);
            var repository = new StoryRepository(context);
            IUnitOfWork unit = new UnitOfWork(context, repository);
            StoryService = new StoryService(unit);
            await ListAllStoriesAsync();
        }

        private static async Task ListAllStoriesAsync()
        {
            foreach (var item in await StoryService.GetAllStoriesAsync())
            {
                Console.WriteLine("##### " + item.Title + " #####  \n" + item.Content);
                Console.WriteLine("####################################################################################");
            }
        }
    }
}
