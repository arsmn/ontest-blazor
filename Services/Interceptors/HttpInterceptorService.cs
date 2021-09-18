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
            _httpClientInterceptor.BeforeSend += InterceptBeforeSend;
            // _httpClientInterceptor.AfterSendAsync += InterceptAftereSendAsync;
        }

        public void DisposeEvent()
        {
            _httpClientInterceptor.BeforeSend -= InterceptBeforeSend;
            // _httpClientInterceptor.AfterSendAsync -= InterceptAftereSendAsync;
        }

        public void InterceptBeforeSend(object sender, HttpClientInterceptorEventArgs e)
        {
            Console.WriteLine("!!!InterceptBeforeSend!!!");
            e.Request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        }

        public void Dispose()
        {
            DisposeEvent();
        }
    }
}