using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Transport.Question
{
    public class CreateQuestionRequest
    {
        public CreateQuestionRequest()
        {
            this.Options = new();
        }

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

        [JsonIgnore]
        private string selectedTag;
        public string SelectedTag
        {
            get => selectedTag;
            set
            {
                this.Options.ForEach(o => o.Answer = false);
                var ans = this.Options.FirstOrDefault(o => o.Tag == value);
                if (ans is not null)
                {
                    ans.Answer = true;
                }
                selectedTag = value;
            }
        }

        public List<CreateOptionRequest> Options { get; set; }

        public void Prepare()
        {
            this.Duration = this.DurationTime.HasValue ? this.DurationTime.Value.TotalSeconds : 0;
        }
    }

    public class CreateOptionRequest
    {
        public CreateOptionRequest()
        {
            this.Tag = Guid.NewGuid().ToString();
        }

        [JsonIgnore]
        public string Tag { get; set; }
        public string Text { get; set; }
        public bool Answer { get; set; }
    }
}