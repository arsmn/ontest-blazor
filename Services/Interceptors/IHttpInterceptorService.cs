using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace OnTest.Blazor.Services.Interceptors
{
    public interface IHttpInterceptorService : IService
    {
        void RegisterEvent();
        void DisposeEvent();
        void InterceptBeforeSend(object sender, HttpClientInterceptorEventArgs e);
    }
}