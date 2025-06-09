using System.ComponentModel.DataAnnotations;

namespace LexiconGruppProject1_grupp7.Web.Views.Stories
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title of the Story")]
        public string StoryTitle { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "Content of the Story")]
        public string StoryContent { get; set; }
    }
}
