using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Pages.Content
{
    public partial class Home
    {
        private MudTable<Transport.Shared.Models.Exam> _table;
        private string _query = string.Empty;

        private void ReloadData()
        {
            _table.ReloadServerData();
        }

        private async Task<TableData<Transport.Shared.Models.Exam>> LoadDataAsync(TableState state)
        {
            var result = await _examService.SearchExamAsync(new Pagination
            {
                Query = _query,
                Page = state.Page + 1,
                PageSize = state.PageSize,
                SortLabel = state.SortLabel,
                SortDirection = state.SortDirection.ToString()
            });
            if (result.Succeeded)
            {
                return new TableData<Transport.Shared.Models.Exam>
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

        private async Task InvokeStartExamModal(Transport.Shared.Models.Exam exam)
        {
            var parameters = new DialogParameters
            {
                { nameof(StartExamModal.ExamId), exam.Id },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await _dialogService.Show<StartExamModal>($"Start << {exam.Title} >> ", parameters, options).Result;
        }
    }
}