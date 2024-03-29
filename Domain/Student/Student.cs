﻿namespace Domain.Student
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? University { get; set; }
        public string? Department { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
