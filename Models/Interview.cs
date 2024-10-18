// Models/Interview.cs
using System;

namespace JobWorld.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? Notes { get; set; }
    }
}
