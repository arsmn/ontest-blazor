using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace OnTest.Blazor.Services.Interceptors
{
    public interface IHttpInterceptorService : IService
    {
        void RegisterEvent();
        void DisposeEvent();
        Task InterceptBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e);
        Task InterceptAftereSendAsync(object sender, HttpClientInterceptorEventArgs e);
    }
}