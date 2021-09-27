using System.Threading.Tasks;
using MudBlazor;
using OnTest.Blazor.Transport.Account;

namespace OnTest.Blazor.Pages.Account
{
    public partial class Devices
    {
        private GetSessionsResponse _model = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var result = await _accountService.GetSessionsAsync();
            if (result.Succeeded)
            {
                _model = result.Data;
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }
    }
}