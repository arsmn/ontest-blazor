using System;
using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Exam
    {
        public long Id { get; set; }
        public long Examiner { get; set; }
        public string Title { get; set; }
        public ExamState State { get; set; }
        public long Duration { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? Deadline { get; set; }
        public bool FreeMovement { get; set; }
        public string Cover { get; set; }
    }

    public enum ExamState
    {
        Draft,
        Published
    }
}