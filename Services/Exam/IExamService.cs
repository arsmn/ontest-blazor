using System.Threading.Tasks;
using OnTest.Blazor.Transport.Exam;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Services.Exam
{
    public interface IExamService : IService
    {
        Task<IResult<Transport.Shared.Models.Exam>> CreateExamAsync(CreateExamRequest request);
    }
}