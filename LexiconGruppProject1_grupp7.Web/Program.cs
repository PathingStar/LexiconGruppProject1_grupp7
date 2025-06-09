
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LexiconGruppProject1_grupp7.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddScoped<IStoryRepository, StoryRepository>();
        builder.Services.AddScoped<IStoryService, StoryService>();
        var connString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));
        

        

        var app = builder.Build();
        app.UseStaticFiles();
        app.MapControllers();

        app.Run();
    }
}
