using Domain.Student;
using Domain.User;
using FinderWebApp.Models.ViewModels.API;

namespace FinderWebApp.Models.ViewModels.Profile
{
    public class StudentProfileViewModel
    {
        public Student Student { get; set; }
        public User User { get; set; }
        public List<University> Universities { get; set; }
        public string Community { get; set; }
    }
}
