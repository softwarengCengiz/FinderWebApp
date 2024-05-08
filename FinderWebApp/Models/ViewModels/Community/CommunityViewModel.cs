namespace FinderWebApp.Models.ViewModels.Community
{
    public class CommunityViewModel
    {
        public Guid CommunityId { get; set; }
        public string? CommunityMembers { get; set; }
        public string? CommunityName { get; set; }
        public string? CommunityDescription { get; set; }
        public string? CommunityImage { get; set; }
        public string? OldEventsIds { get; set; }
        public Guid CreatorUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifierUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? EventId { get; set; } //Sadece oylama başlatmada kullanılır.
    }
}
