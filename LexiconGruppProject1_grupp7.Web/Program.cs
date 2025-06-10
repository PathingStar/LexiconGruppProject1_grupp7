
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance.Repositories;
using Microsoft.AspNetCore.Identity;
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
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        var connString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

        builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));
        

        

        var app = builder.Build();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapControllers();

        app.Run();
    }
}
