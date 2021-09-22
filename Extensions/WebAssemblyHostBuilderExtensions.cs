using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using OnTest.Blazor.Authentication;
using OnTest.Blazor.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using FluentValidation;
using Validators;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OnTest.Blazor.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace OnTest.Blazor.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "OnTest.API";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services
                .AddAuthorizationCore()
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = false;
                })
                .AddScoped<AuthenticationStateProvider, HostStateProvider>()
                .AddServices()
                .AddValidators()
                .AddLazyCache()
                .Configure<AppConfig>(options => builder.Configuration.GetSection("OnTest").Bind(options))
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(builder.Configuration["OnTest:BaseAddress"]);
                }).AddHttpMessageHandler(() => new DefaultBrowserOptionsMessageHandler
                {
                    DefaultBrowserRequestMode = BrowserRequestMode.Cors,
                    DefaultBrowserRequestCredentials = BrowserRequestCredentials.Include
                });
            builder.Services.AddHttpClientInterceptor();

            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var managers = typeof(IService);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                    services.AddTransient(type.Service, type.Implementation);
            }

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssemblyContaining<Validator>();
        }
    }
}