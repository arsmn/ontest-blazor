using System.Text.Json;
using Humanizer;

namespace OnTest.Blazor.Extensions
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance => new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return name.Underscore();
        }
    }
}