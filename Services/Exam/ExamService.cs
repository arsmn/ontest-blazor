using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Transport.Exam;
using OnTest.Blazor.Transport.Question;
using OnTest.Blazor.Transport.Shared.Models;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Exam
{
    public class ExamService : IExamService
    {
        private readonly HttpClient _httpClient;

        public ExamService(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));
        }


        public async Task<IResult<Transport.Shared.Models.Exam>> GetExamAsync(long id)
        {
            var response = await _httpClient.GetAsync($"exam/{id}");
            var result = await response.ToResult<Transport.Shared.Models.Exam>();
            return result;
        }

        public async Task<IResult<Transport.Shared.Models.Exam>> CreateExamAsync(CreateExamRequest request)
        {
            request.Prepare();
            var response = await _httpClient.PostAsJsonAsync("exam", request, JsonExtensions.Options);
            var result = await response.ToResult<Transport.Shared.Models.Exam>();
            return result;
        }

        public async Task<IResult> UpdateExamAsync(CreateExamRequest request)
        {
            request.Prepare();
            var response = await _httpClient.PutAsJsonAsync($"exam/{request.Id}", request, JsonExtensions.Options);
            var result = await response.ToResult<Transport.Shared.Models.Exam>();
            return result;
        }

        public async Task<IResult<Transport.Shared.Models.Exam>> SetCoverAsync(MultipartFormDataContent content, long id)
        {
            var response = await _httpClient.PostAsync($"exam/{id}/cover", content);
            var result = await response.ToResult<Transport.Shared.Models.Exam>();
            return result;
        }

        public async Task<IResult> DeleteCoverAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"exam/{id}/cover");
            var result = await response.ToResult();
            return result;
        }

        public async Task<Paginated<Question>> GetQuestionsAsync(long id, Pagination pagination)
        {
            var response = await _httpClient.GetAsync(pagination.BuildUrl($"exam/{id}/questions"));
            var result = await response.ToPaginated<Question>();
            return result;
        }

        public async Task<IResult<Question>> CreateQuestionAsync(CreateQuestionRequest request)
        {
            request.Prepare();
            var response = await _httpClient.PostAsJsonAsync($"exam/{request.ExamId}/question", request);
            var result = await response.ToResult<Question>();
            return result;
        }

        public async Task<IResult> UpdateQuestionAsync(CreateQuestionRequest request)
        {
            request.Prepare();
            var response = await _httpClient.PutAsJsonAsync($"exam/{request.ExamId}/question/{request.Id}", request);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult> DeleteQuestionAsync(long eid, long qid)
        {
            var response = await _httpClient.DeleteAsync($"exam/{eid}/question/{qid}");
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult<List<Option>>> GetQuestionOptionsAsync(long eid, long qid)
        {
            var response = await _httpClient.GetAsync($"exam/{eid}/question/{qid}/options");
            var result = await response.ToResult<List<Option>>();
            return result;
        }

        public async Task<IResult> PublishExamAsync(long eid)
        {
            var response = await _httpClient.PostAsync($"exam/{eid}/publish", null);
            var result = await response.ToResult();
            return result;
        }

        public async Task<IResult<ExamStatsResponse>> GetExamStatsAsync(long eid)
        {
            var response = await _httpClient.GetAsync($"exam/{eid}/stats");
            var result = await response.ToResult<ExamStatsResponse>();
            return result;
        }

        public async Task<Paginated<Transport.Shared.Models.Exam>> SearchExamAsync(Pagination pagination)
        {
            var response = await _httpClient.GetAsync(pagination.BuildUrl($"exam/search"));
            var result = await response.ToPaginated<Transport.Shared.Models.Exam>();
            return result;
        }

        public async Task<IResult<ExamResult>> ParticipateExam(long eid)
        {
            var response = await _httpClient.PostAsync($"exam/{eid}/participate", null);
            var result = await response.ToResult<ExamResult>();
            return result;
        }

        public async Task<IResult<ExamResult>> GetExamResult(long rid)
        {
            var response = await _httpClient.GetAsync($"result/{rid}");
            var result = await response.ToResult<ExamResult>();
            return result;
        }

        public async Task<IResult> SubmitAnswer(SubmitAnswerRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"result/{request.Id}/submit", request, JsonExtensions.Options);
            var result = await response.ToResult();
            return result;
        }
    }
}