﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Participant
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int JobType { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
        public string? CV { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
