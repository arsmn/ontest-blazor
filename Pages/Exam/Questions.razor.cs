using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Pages.Exam
{
    public partial class Questions
    {
        [Parameter] public long ExamId { get; set; }

        private MudTable<Question> _table;

        private void ReloadData()
        {
            _table.ReloadServerData();
        }

        private async Task<TableData<Question>> LoadDataAsync(TableState state)
        {
            var result = await _examService.GetQuestionsAsync(ExamId, new Pagination
            {
                Page = state.Page + 1,
                PageSize = state.PageSize,
                SortLabel = state.SortLabel,
                SortDirection = state.SortDirection.ToString()
            });
            if (result.Succeeded)
            {
                return new TableData<Question>
                {
                    Items = result.Data,
                    TotalItems = result.Pager.TotalCount,
                };
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
                return null;
            }
        }

        private async Task InvokeCreateQuestionModal()
        {
            var parameters = new DialogParameters
            {
                { nameof(QuestionModal.State), QuestionModalState.Create },
                { nameof(QuestionModal.ExamId), this.ExamId }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<QuestionModal>("Create new question", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
                ReloadData();
        }

        private async Task InvokeUpdateQuestionModal(Question question)
        {
            var parameters = new DialogParameters
            {
                { nameof(QuestionModal.State), QuestionModalState.Update },
                { nameof(QuestionModal.ExamId), this.ExamId },
                { nameof(QuestionModal.Question), question }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = await _dialogService.Show<QuestionModal>("Update question", parameters, options).Result;
            if (!dialog.Cancelled)
                ReloadData();
        }

        private async Task InvokeDeleteQuestionModal(Question question)
        {
            var parameters = new DialogParameters
                {
                    { nameof(Shared.Dialogs.Confirmation.Title), "Delete Question" },
                    { nameof(Shared.Dialogs.Confirmation.Content), "Are you sure you want to delete this question" },
                    { nameof(Shared.Dialogs.Confirmation.Icon), Icons.Material.Filled.Help },
                    { nameof(Shared.Dialogs.Confirmation.Color), Color.Error },
                };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await _dialogService.Show<Shared.Dialogs.Confirmation>("Delete", parameters, options).Result;
            if (!dialog.Cancelled)
            {
                var result = await _examService.DeleteQuestionAsync(this.ExamId, question.Id);
                if (result.Succeeded)
                {
                    _snackBar.Add("Question deleted!", Severity.Success);
                    ReloadData();
                }
                else
                {
                    _snackBar.Add(result.Error.Message, Severity.Error);
                }
            }
        }
    }
}