using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Transport.Question
{
    public class CreateQuestionRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
        public long ExamId { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public double Duration { get; set; }

        [JsonIgnore]
        public TimeSpan? DurationTime { get; set; }
        public int Score { get; set; }
        public int NegativeScore { get; set; }
        public List<CreateOptionRequest> Options { get; set; }

        public void Prepare()
        {
            this.Duration = this.DurationTime.HasValue ? this.DurationTime.Value.TotalSeconds : 0;
        }
    }

    public class CreateOptionRequest
    {
        public string Text { get; set; }
        public bool Answer { get; set; }
    }
}