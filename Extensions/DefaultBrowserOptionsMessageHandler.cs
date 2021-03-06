using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace OnTest.Blazor.Extensions
{
    public sealed class DefaultBrowserOptionsMessageHandler : DelegatingHandler
    {
        private static readonly HttpRequestOptionsKey<IDictionary<string, object>> _httpRequestOptionsKey = new("WebAssemblyFetchOptions");

        public DefaultBrowserOptionsMessageHandler()
        {
        }

        public DefaultBrowserOptionsMessageHandler(HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
        }

        public BrowserRequestCache DefaultBrowserRequestCache { get; set; }
        public BrowserRequestCredentials DefaultBrowserRequestCredentials { get; set; }
        public BrowserRequestMode DefaultBrowserRequestMode { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the existing options to not override them if set explicitly
            if (!request.Options.TryGetValue(_httpRequestOptionsKey, out var fetchOptions))
            {
                fetchOptions = null;
            }

            if (fetchOptions?.ContainsKey("cache") != true)
            {
                request.SetBrowserRequestCache(DefaultBrowserRequestCache);
            }

            if (fetchOptions?.ContainsKey("credentials") != true)
            {
                request.SetBrowserRequestCredentials(DefaultBrowserRequestCredentials);
            }

            if (fetchOptions?.ContainsKey("mode") != true)
            {
                request.SetBrowserRequestMode(DefaultBrowserRequestMode);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}