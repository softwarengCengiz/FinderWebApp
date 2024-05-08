namespace FinderWebApp.Models.Request.Polling
{
    public class StartPollingRequest
    {
        public Guid CommunityId { get; set; }
        public Guid EventId { get; set; }
        public bool IsActive { get; set; }
    }
}
