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
                StartAt = StartAt.Value.Add(StartAtTime.Value);
                StartAt = StartAt.Value.ToUniversalTime();
            }


            if (Deadline.HasValue && DeadlineTime.HasValue)
            {
                Deadline = Deadline.Value.Add(DeadlineTime.Value);
                Deadline = Deadline.Value.ToUniversalTime();
            }

            if (Once)
            {
                Deadline = null;
                DeadlineTime = null;
            }
        }
    }
}