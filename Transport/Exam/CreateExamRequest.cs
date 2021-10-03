using System;
using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Exam
{
    public class CreateExamRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Title { get; set; }
        public bool FreeMovement { get; set; }
        public DateTime? StartAt { get; set; }

        [JsonIgnore]
        public TimeSpan? StartAtTime { get; set; }
        public bool Once { get; set; }
        public DateTime? Deadline { get; set; }

        [JsonIgnore]
        public TimeSpan? DeadlineTime { get; set; }

        public void Prepare()
        {
            if (StartAt.HasValue && StartAtTime.HasValue)
            {
                StartAt = new DateTime(StartAt.Value.Year,
                    StartAt.Value.Month,
                    StartAt.Value.Day,
                    StartAtTime.Value.Hours,
                    StartAtTime.Value.Minutes,
                    StartAtTime.Value.Seconds, DateTimeKind.Local);
            }


            if (Deadline.HasValue && DeadlineTime.HasValue)
            {
                Deadline = new DateTime(Deadline.Value.Year,
                    Deadline.Value.Month,
                    Deadline.Value.Day,
                    DeadlineTime.Value.Hours,
                    DeadlineTime.Value.Minutes,
                    DeadlineTime.Value.Seconds, DateTimeKind.Local);
            }

            if (Once)
            {
                Deadline = null;
                DeadlineTime = null;
            }
        }
    }
}