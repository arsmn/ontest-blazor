using System;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Exam
    {
        public long Id { get; set; }
        public long Examiner { get; set; }
        public string Title { get; set; }
        public ExamState State { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? Deadline { get; set; }
        public bool FreeMove { get; set; }
    }

    public enum ExamState
    {
        Unknown,
        Draft,
        Published
    }
}