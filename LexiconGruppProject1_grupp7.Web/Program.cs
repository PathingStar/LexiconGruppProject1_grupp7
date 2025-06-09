
using LexiconGruppProject1_grupp7.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace LexiconGruppProject1_grupp7.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
