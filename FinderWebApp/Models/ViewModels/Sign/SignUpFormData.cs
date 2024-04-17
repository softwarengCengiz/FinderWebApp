namespace FinderWebApp.Models.ViewModels.Sign
{
    public class SignUpFormData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Re_Password { get; set; }
        public int Role { get; set; }
    }
}
