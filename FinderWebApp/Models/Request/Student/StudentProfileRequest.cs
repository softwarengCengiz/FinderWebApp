namespace FinderWebApp.Models.Request.Student
{
    public class StudentProfileRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? University { get; set; }
        public string? Department { get; set; }
    }
}
