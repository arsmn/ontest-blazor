using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Toolbelt.Blazor;

namespace OnTest.Blazor.Services.Interceptors
{
    public class HttpInterceptorService : IHttpInterceptorService, IDisposable
    {
        private readonly HttpClientInterceptor _httpClientInterceptor;

        public HttpInterceptorService(
            HttpClientInterceptor httpClientInterceptor
        )
        {
            _httpClientInterceptor = httpClientInterceptor ??
                throw new ArgumentNullException(nameof(httpClientInterceptor));
            
            RegisterEvent();
        }

        public void RegisterEvent()
        {
            _httpClientInterceptor.BeforeSendAsync += InterceptBeforeSendAsync;
            _httpClientInterceptor.AfterSendAsync += InterceptAftereSendAsync;
        }

        public void DisposeEvent()
        {
            _httpClientInterceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
            _httpClientInterceptor.AfterSendAsync -= InterceptAftereSendAsync;
        }

        public async Task InterceptBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            e.Request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        }

        public async Task InterceptAftereSendAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            
        }

        public void Dispose()
        {
            DisposeEvent();
        }
    }
}