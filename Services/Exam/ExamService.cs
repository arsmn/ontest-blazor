using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnTest.Blazor.Extensions;
using OnTest.Blazor.Transport.Exam;
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

        public async Task<IResult> UpdateExamAsync(UpdateExamRequest request)
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
    }
}