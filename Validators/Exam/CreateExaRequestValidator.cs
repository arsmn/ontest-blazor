using System;
using FluentValidation;
using OnTest.Blazor.Transport.Exam;

namespace OnTest.Blazor.Validators.Exam
{
    public class CreateExaRequestValidator : AbstractValidator<CreateExamRequest>
    {
        public CreateExaRequestValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(5).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(50).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.StartAt)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required")
                .Must((obj, startAt) => DateTime.Compare(startAt.Value, DateTime.Now.Date) >= 0)
                .WithMessage("{PropertyName} should not be before now");
            RuleFor(request => request.StartAtTime)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required")
                .Must((obj, startAtTime) =>
                {
                    if (!obj.StartAt.HasValue)
                        return true;

                    int res = DateTime.Compare(obj.StartAt.Value, DateTime.Now.Date);
                    switch (res)
                    {
                        case -1:
                            return false;
                        case 0:
                            return TimeSpan.Compare(startAtTime.Value, DateTime.Now.TimeOfDay) >= 0;
                        case 1:
                            return true;
                        default:
                            return true;
                    }
                })
                .WithMessage("{PropertyName} should not be before now");
            RuleFor(request => request.Deadline)
                .Cascade(CascadeMode.Stop)
                .Must((obj, deadline) => deadline.HasValue || obj.Once)
                .WithMessage("{PropertyName} is required")
                .Must((obj, deadline) =>
                {
                    if (obj.Once ||
                        !obj.StartAt.HasValue ||
                        !deadline.HasValue)
                        return true;

                    return DateTime.Compare(deadline.Value, obj.StartAt.Value) >= 0;
                })
                .WithMessage("{PropertyName} should not be before start date");
            RuleFor(request => request.DeadlineTime)
                .Cascade(CascadeMode.Stop)
                .Must((obj, deadlineTime) => deadlineTime.HasValue || obj.Once)
                .WithMessage("{PropertyName} is required")
                .Must((obj, deadlineTime) =>
                {
                    if (!obj.DeadlineTime.HasValue ||
                        !obj.DeadlineTime.HasValue ||
                        !obj.StartAtTime.HasValue)
                        return true;

                    int res = DateTime.Compare(obj.StartAt.Value, obj.Deadline.Value);
                    if (res == 0)
                        return TimeSpan.Compare(deadlineTime.Value, obj.StartAtTime.Value) >= 0;

                    return true;
                })
                .WithMessage("{PropertyName} should not be before start date");
        }
    }
}