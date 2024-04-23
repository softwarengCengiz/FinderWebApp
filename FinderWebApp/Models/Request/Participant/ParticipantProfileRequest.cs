namespace FinderWebApp.Models.Request.Participant
{
    public class ParticipantProfileRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Job { get; set; }
        public string? CV { get; set; }  
    }
}
