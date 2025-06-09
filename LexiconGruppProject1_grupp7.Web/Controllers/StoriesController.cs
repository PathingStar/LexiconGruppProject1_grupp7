using LexiconGruppProject1_grupp7.Application.Stories.Interfaces;
using LexiconGruppProject1_grupp7.Domain.Entities;
using LexiconGruppProject1_grupp7.Web.Views.Stories;
using Microsoft.AspNetCore.Mvc;

namespace LexiconGruppProject1_grupp7.Web.Controllers
{
    public class StoriesController(IStoryService service) : Controller
    {
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var stories = await service.GetAllStoriesAsync();
            var viewModel = new IndexVM
            {
                StoryItems = stories.Select(s => new IndexVM.StoryItemVM
                {
                    StoryTitle = s.Title,
                    StoryId = s.Id
                }).ToArray()
            };

            return View(viewModel);
        }
        [Route("stories/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var story = await service.GetStoryByIdAsync(id);

            if (story == null)
            {
                return NotFound();
            }
            var viewModel = new DetailsVM
            {

                StoryTitle = story.Title,
                StoryContent = story.Content,

            };
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await service.AddStoryAsync(new Story
            {
                Title = model.StoryTitle,
                Content = model.StoryContent
            });
            // Here you would typically call a service to save the story
            // await service.CreateStoryAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
