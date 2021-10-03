using System.Collections.Generic;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class ExamResult
    {
        public ExamResult()
        {
            Answers = new();
        }

        public long Id { get; set; }
        public long Examinee { get; set; }
        public long ExamId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}