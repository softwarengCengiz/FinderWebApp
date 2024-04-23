namespace FinderWebApp.Models.ApiRequest.Sign
{
    public class SignUpRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
