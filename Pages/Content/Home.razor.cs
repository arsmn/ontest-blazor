using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace OnTest.Blazor.Pages.Content
{
    public partial class Home
    {
        [Parameter] public int AvailableTestsCount { get; set; }
        [Parameter] public int TakenTestsCount { get; set; }
        [Parameter] public int UpcomingTestsCount { get; set; }
        [Parameter] public int CertificatesCount { get; set; }

        private bool _loaded = true;

        protected override Task OnInitializedAsync()
        {
            _loaded = true;
            return Task.FromResult(true);
        }
    }
}