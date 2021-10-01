using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Pages.Exam
{
    public partial class Questions
    {
        [Parameter] public long Id { get; set; }

        private MudTable<Question> _table;

        private void ReloadData()
        {
            _table.ReloadServerData();
        }

        private async Task<TableData<Question>> LoadDataAsync(TableState state)
        {
            var result = await _examService.GetQuestionsAsync(Id, new Pagination
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
                { nameof(QuestionModal.ExamId), this.Id }
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
                { nameof(QuestionModal.ExamId), this.Id },
                { nameof(QuestionModal.Question), question }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<QuestionModal>("Update question", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
                ReloadData();
        }
    }
}