using System.Threading.Tasks;
using Blazored.FluentValidation;
using MudBlazor;
using OnTest.Blazor.Transport.Exam;

namespace OnTest.Blazor.Pages.Exam
{
    public partial class Create
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private CreateExamRequest _model = new();
        private bool _processing;

        private async Task SubmitAsync()
        {
            _processing = true;
            var result = await _examService.CreateExamAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Exam created successfully", Severity.Success);
                _navigationManager.NavigateTo($"/exam/{result.Data.Id}");
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }
    }
}