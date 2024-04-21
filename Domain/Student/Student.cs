namespace Domain.Student
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? University { get; set; }
        public string? Department { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
