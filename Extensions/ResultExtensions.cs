using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OnTest.Blazor.Transport.Shared.Wrapper;

namespace OnTest.Blazor.Extensions
{
    internal static class JsonExtensions
    {
        internal static JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        };
    
        internal static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response)
        {
            return await response.Content.ReadFromJsonAsync<Result<T>>(Options);
        }

        internal static async Task<IResult> ToResult(this HttpResponseMessage response)
        {
            return await response.Content.ReadFromJsonAsync<Result>(Options);
        }

        internal static async Task<Paginated<T>> ToPaginated<T>(this HttpResponseMessage response)
        {
            return await response.Content.ReadFromJsonAsync<Paginated<T>>(Options);
        }
    }
}