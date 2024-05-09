namespace FinderWebApp.Models.Request.Polling
{
    public class VoteToEventRequest
    {
        public Guid EventId { get; set; }
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
    }
}
