namespace LexiconGruppProject1_grupp7.Web.Views.Stories
{
    public class IndexVM
    {
        public required StoryItemVM[] StoryItems {  get; set; }

        public class StoryItemVM
        {
            public required string StoryTitle { get; set; }
            public required int StoryId { get; set; }
        }
    }
}
