using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Answer
    {
        public Answer()
        {
            SelectedOptions = new();
        }

        public long Id { get; set; }
        public Question Question { get; set; }
        public long ResultId { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        private long selectedTag;
        public long SelectedTag
        {
            get => selectedTag;
            set
            {
                SelectedOptions.ForEach(o => o.Answer = false);
                SelectedOptions.First(o => o.OptionId == value).Answer = true;
                selectedTag = value;
            }
        }

        private List<SelectedOption> selectedOptions;
        public List<SelectedOption> SelectedOptions
        {
            get => selectedOptions;
            set
            {
                var s = value.FirstOrDefault(o => o.Answer == true);
                if (s is not null)
                    selectedTag = s.OptionId;
                selectedOptions = value;
            }
        }
    }

    public class SelectedOption
    {
        public long OptionId { get; set; }
        public bool Answer { get; set; }
    }
}