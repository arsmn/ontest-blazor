using System;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Question;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Pages.Exam
{
    public partial class QuestionModal
    {
        [Parameter] public QuestionModalState State { get; set; }
        [Parameter] public long ExamId { get; set; }
        [Parameter] public Question Question { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private CreateQuestionRequest _model = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await _examService.GetQuestionOptionsAsync(ExamId, Question.Id);
            if (result.Succeeded)
            {
                _model.Options.AddRange(result.Data.Select(o => new CreateOptionRequest
                {
                    Text = o.Text,
                    Answer = o.Answer
                }));
                _model.SelectedTag = _model.Options?.FirstOrDefault(o => o.Answer == true)?.Tag;
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        protected override void OnParametersSet()
        {
            _model.ExamId = ExamId;
            if (State == QuestionModalState.Update)
            {
                _model.Id = this.Question.Id;
                _model.Text = this.Question.Text;
                _model.Type = this.Question.Type;
                _model.DurationTime = this.Question.DurationTime;
                _model.Score = this.Question.Score;
                _model.NegativeScore = this.Question.NegativeScore;
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void AddOption()
        {
            if (_model.Options.Count < 10)
            {
                _model.Options.Add(new CreateOptionRequest());
            }
            else
            {
                _snackBar.Add("Maximum options reached!", Severity.Warning);
            }
        }

        private void RemoveOption(string tag)
        {
            _model.Options.Remove(_model.Options.First(o => o.Tag == tag));
        }

        private async Task SubmitAsync()
        {
            switch (State)
            {
                case QuestionModalState.Create:
                    await CreateAsync();
                    break;
                case QuestionModalState.Update:
                    await UpdateAsync();
                    break;
            }
        }

        private async Task CreateAsync()
        {
            var result = await _examService.CreateQuestionAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Question created!", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        private async Task UpdateAsync()
        {
            var result = await _examService.UpdateQuestionAsync(_model);
            if (result.Succeeded)
            {
                _snackBar.Add("Question updated!", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

    }

    public enum QuestionModalState
    {
        Create,
        Update
    }
}