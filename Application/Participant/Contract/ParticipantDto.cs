

namespace Application.Participant.Contract
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
        public string? CV { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
