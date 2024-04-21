using Domain.Student;
using Domain.User;

namespace FinderWebApp.Models.ViewModels.Profile
{
    public class StudentProfileViewModel
    {
        public Student Student { get; set; }
        public User User { get; set; }
    }
}
