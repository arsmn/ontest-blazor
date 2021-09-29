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

        public async Task<IResult<Transport.Shared.Models.Exam>> CreateExamAsync(CreateExamRequest request)
        {
            request.Prepare();
            var response = await _httpClient.PostAsJsonAsync("exam", request, JsonExtensions.Options);
            var result = await response.ToResult<Transport.Shared.Models.Exam>();
            return result;
        }
    }
}