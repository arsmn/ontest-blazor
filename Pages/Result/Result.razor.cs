using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnTest.Blazor.Transport.Shared.Models;
using Humanizer;
using System.Timers;
using System.Linq;

namespace OnTest.Blazor.Pages.Result
{
    public partial class Result : IDisposable
    {
        [Parameter] public long Id { get; set; }
        private ExamResult _model = new();
        private string _duration;
        private string _totalScores;
        private string _totalQuestionsStr;
        private long _totalQuestions;
        private string _totalNegativeScores;
        private Timer _timer;
        private DateTime _endDate;
        private bool _disabled;
        private int _activeIndex;
        private bool _processing;
        private bool _reviewMode;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await LoadStatsAsync();
        }

        private void SetCurrentTab()
        {
            if (_disabled)
            {
                for (int i = 0; i < _model.Answers.Count; i++)
                {
                    if (_model.Answers[i].Active)
                    {
                        if (i == _model.Answers.Count - 1)
                        {
                            _activeIndex = i;
                        }
                        else
                        {
                            _activeIndex = i + 1;
                        }
                    }
                }
            }
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
                _reviewMode = DateTime.Now.CompareTo(_endDate) > 0;

                if (!_reviewMode)
                {
                    _timer = new Timer(1000);
                    _timer.Elapsed += Counter;
                    _timer.Enabled = true;
                }
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
        }

        private void Counter(Object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.CompareTo(_endDate) > 0)
            {
                _reviewMode = true;
            }
            _duration = _endDate.Subtract(DateTime.Now).Humanize(2);
            StateHasChanged();
        }

        private async Task SubmitAsync(Answer answer)
        {
            _processing = true;
            var result = await _examService.SubmitAnswer(new Transport.Exam.SubmitAnswerRequest
            {
                Id = answer.Id,
                Text = answer.Text,
                SelectedOptions = answer.SelectedOptions.ToArray()
            });
            if (result.Succeeded)
            {
                if (_disabled)
                {
                    _model.Answers.First(a => a.Id == answer.Id).Active = true;
                }
                else
                {
                    if (_activeIndex < _model.Answers.Count - 1)
                    {
                        _activeIndex++;
                    }
                }
                if (_activeIndex < _totalQuestions - 1)
                {

                }
                else
                {
                    _snackBar.Add("Finished! Good Luck!", Severity.Info);
                }
                _snackBar.Add("Answer submitted!", Severity.Success);
            }
            else
            {
                _snackBar.Add(result.Error.Message, Severity.Error);
            }
            _processing = false;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}