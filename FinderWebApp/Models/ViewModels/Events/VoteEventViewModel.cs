using FinderWebApp.Models.ViewModels.Community;
using FinderWebApp.Models.ViewModels.Polling;

namespace FinderWebApp.Models.ViewModels.Events
{
    public class VoteEventViewModel
    {
        public MyEventsViewModel Event { get; set; }
        public List<PollingViewModel> Pollings { get; set; }
        public List<CommunityViewModel> Communities { get; set; }
        public bool IsCommunityOwner { get; set; }
    }
}
