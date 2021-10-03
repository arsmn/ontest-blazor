using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Exam;
using OnTest.Blazor.Transport.Question;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Exam
{
    public interface IExamService : IService
    {
        Task<IResult<Transport.Shared.Models.Exam>> GetExamAsync(long id);
        Task<IResult<Transport.Shared.Models.Exam>> CreateExamAsync(CreateExamRequest request);
        Task<IResult> UpdateExamAsync(CreateExamRequest request);
        Task<IResult<Transport.Shared.Models.Exam>> SetCoverAsync(MultipartFormDataContent content, long id);
        Task<IResult> DeleteCoverAsync(long id);
        Task<Paginated<Question>> GetQuestionsAsync(long id, Pagination pagination);
        Task<IResult<Question>> CreateQuestionAsync(CreateQuestionRequest request);
        Task<IResult> UpdateQuestionAsync(CreateQuestionRequest request);
        Task<IResult> DeleteQuestionAsync(long eid, long qid);
        Task<IResult<List<Option>>> GetQuestionOptionsAsync(long eid, long qid);
        Task<IResult<ExamStatsResponse>> GetExamStatsAsync(long eid);
        Task<IResult> PublishExamAsync(long eid);
        Task<Paginated<Transport.Shared.Models.Exam>> SearchExamAsync(Pagination pagination);
        Task<IResult<ExamResult>> ParticipateExam(long eid);
        Task<IResult<ExamResult>> GetExamResult(long rid);
        Task<IResult> SubmitAnswer(SubmitAnswerRequest request);
    }
}