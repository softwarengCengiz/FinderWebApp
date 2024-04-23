using Domain.Participant;
using Domain.User;

namespace FinderWebApp.Models.ViewModels.Profile
{
    public class ParticipantProfileViewModel
    {
        public Participant Participant { get; set; }
        public User User { get; set; }
    }
}
