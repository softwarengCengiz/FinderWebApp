namespace FinderWebApp.Models.Request.Community
{
    public class CreateCommunityRequest
    {
        public string CommunityName { get; set; }
        public string CommunityDescription { get; set; }
        public IFormFile CommunityImage { get; set; }
    }
}
