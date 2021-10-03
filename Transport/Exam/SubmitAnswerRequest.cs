using System.Collections.Generic;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Transport.Exam
{
    public class SubmitAnswerRequest
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public SelectedOption[] SelectedOptions { get; set; }
    }
}