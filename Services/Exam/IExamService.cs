using System.Net.Http;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Exam;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Exam
{
    public interface IExamService : IService
    {
        Task<IResult<Transport.Shared.Models.Exam>> GetExamAsync(long id);
        Task<IResult<Transport.Shared.Models.Exam>> CreateExamAsync(CreateExamRequest request);
        Task<IResult> UpdateExamAsync(UpdateExamRequest request);
        Task<IResult<Transport.Shared.Models.Exam>> SetCoverAsync(MultipartFormDataContent content, long id);
        Task<IResult> DeleteCoverAsync(long id);
    }
}