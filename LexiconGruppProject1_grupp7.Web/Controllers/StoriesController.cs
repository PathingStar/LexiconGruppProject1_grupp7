using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiconGruppProject1_grupp7.Web.Controllers
{
    public class StoriesController(IStoryService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var stories = await service.GetAllStoriesAsync();
            return View(stories);
        }
    }
}
