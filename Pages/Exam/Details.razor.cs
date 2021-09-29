using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using OnTest.Blazor.Transport.Exam;

namespace OnTest.Blazor.Pages.Exam
{
    public partial class Details
    {
        [Parameter] public long Id { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly UpdateExamRequest _model = new();

        private bool _processing;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _processing = true;
            var result = await _examService.GetExamAsync(Id);
            if (result.Succeeded)
            {
                _model.Title = result.Data.Title;
                _model.StartAt = result.Data.StartAt;
                _model.StartAtTime = result.Data.StartAt.TimeOfDay;
                _model.Deadline = result.Data.Deadline;
                _model.DeadlineTime = result.Data.Deadline.HasValue ? result.Data.Deadline.Value.TimeOfDay : null;
                _model.Once = !_model.Deadline.HasValue;
                _model.FreeMovement = result.Data.FreeMovement;

                _examCover = !string.IsNullOrEmpty(result.Data.Cover) ? $"{result.Data.Cover}?hash={Guid.NewGuid().ToString()}" : "";
                _cardCover = !string.IsNullOrEmpty(result.Data.Cover) ? $"{result.Data.Cover}?hash={Guid.NewGuid().ToString()}" : "";
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        private async Task SubmitAsync()
        {
            _processing = true;
            var result = await _examService.UpdateExamAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Exam updated!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        private string _tmpCover;
        private string _examCover;
        private string _cardCover;
        private bool _coverProcessing;
        private IBrowserFile _coverFile;

        private async Task SelectCover(InputFileChangeEventArgs args)
        {
            if (args.FileCount == 0)
            {
                _snackBar.Add("Please select an image", Severity.Warning);
                return;
            }

            _coverFile = args.File;
            byte[] buffer = new byte[_coverFile.Size];
            await _coverFile.OpenReadStream(10485760).ReadAsync(buffer);
            _tmpCover = $"data:{_coverFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
            _cardCover = _tmpCover;
        }

        private async Task UploadCover()
        {
            if (_coverFile is null)
            {
                _snackBar.Add("Please select an image", Severity.Warning);
                return;
            }

            _coverProcessing = true;

            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(_coverFile.OpenReadStream(10485760));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(_coverFile.ContentType);
            content.Add(
                content: fileContent,
                name: "\"file\"",
                fileName: _coverFile.Name);
            var result = await _examService.SetCoverAsync(content, Id);
            if (result.Succeeded)
            {
                _examCover = _tmpCover;
                _cardCover = _tmpCover;
                _tmpCover = string.Empty;
                _snackBar.Add("Cover changed!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _coverProcessing = false;
        }

        private async Task DeleteCover()
        {
            _coverProcessing = true;
            var result = await _examService.DeleteCoverAsync(Id);
            if (result.Succeeded)
            {
                _tmpCover = string.Empty;
                _examCover = string.Empty;
                _cardCover = string.Empty;
                _snackBar.Add("Cover deleted!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _coverProcessing = false;
        }

        private void ClearCover()
        {
            _tmpCover = string.Empty;
            _cardCover = _examCover;
        }
    }
}
