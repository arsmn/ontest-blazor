using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Humanizer;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Question
    {
        public long Id { get; set; }
        public long ExamId { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public long Duration { get; set; }
        public int Score { get; set; }
        public int NegativeScore { get; set; }
        public List<Option> Options { get; set; }

        public TimeSpan DurationTime => TimeSpan.FromSeconds(this.Duration);
        public string DurationTimeString => DurationTime.Humanize(2);
    }

    public enum QuestionType
    {
        Descriptive,
        SingleChoice,
        MultipleChoice
    }
}