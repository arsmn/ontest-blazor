using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;

namespace OnTest.Blazor.Pages.Result
{
    public partial class Assessment
    {
        [Parameter] public long Id { get; set; }

        private ExamResult _model = new();
        private string _totalScores;
        private string _totalQuestionsStr;
        private long _totalQuestions;
        private string _totalNegativeScores;
        private DateTime _endDate;
        private bool _disabled;
        private int _activeIndex;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await LoadStatsAsync();
        }

        private async Task LoadDataAsync()
        {
            var result = await _examService.GetExamResult(Id);
            if (result.Succeeded)
            {
                _model = result.Data;
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        private async Task LoadStatsAsync()
        {
            var eResult = await _examService.GetExamAsync(_model.ExamId);
            if (!eResult.Succeeded)
            {
                _disabled = !eResult.Data.FreeMovement;
                _snackBar.Add(eResult.Error.Message, Severity.Error);
            }

            var result = await _examService.GetExamStatsAsync(_model.ExamId);
            if (result.Succeeded)
            {
                _endDate = eResult.Data.StartAt.Add(TimeSpan.FromSeconds(result.Data.TotalDuration));
                _totalScores = result.Data.TotalScores.ToString() + "+";
                _totalNegativeScores = result.Data.TotalNegativeScores.ToString() + "-";
                _totalQuestionsStr = result.Data.TotalQuestions.ToString();
                _totalQuestions = result.Data.TotalQuestions;
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        private void Next()
        {
            _activeIndex++;
        }
    }
}