
using LexiconGruppProject1_grupp7.Application.Dtos;
using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Application.Stories.Services;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using LexiconGruppProject1_grupp7.Infrastructure.Presistance.Repositories;
using LexiconGruppProject1_grupp7.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LexiconGruppProject1_grupp7.Web;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IStoryRepository, StoryRepository>();
        builder.Services.AddScoped<IStoryService, StoryService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IIdentityUserService, IdentityUserSevice>();

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
        builder.Services.ConfigureApplicationCookie(o => o.LogoutPath = "");

        builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));


        var app = builder.Build();

        // Kör seeding (en gång vid uppstart)
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var service = services.GetRequiredService<IUserService>();
            await service.CreateUserAsync(new UserProfileDto
             (
                  "Test@mail.com",
                  "TestApp"
             ), "testpassword");

            var service2 = services.GetRequiredService<IIdentityUserService>();
            ApplicationUser applicationUser = new ApplicationUser();

            //service2.CreateUserAsync(applicationUser)
        }
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();




        app.Run();
    }
}
