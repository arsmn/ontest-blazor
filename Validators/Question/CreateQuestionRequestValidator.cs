using System.Linq;
using FluentValidation;
using OnTest.Blazor.Transport.Question;

namespace OnTest.Blazor.Validators.Question
{
    public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionRequestValidator()
        {
            RuleFor(request => request.Text)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(1).WithMessage("{PropertyName} must be at least of length {MinLength}")
                .MaximumLength(250).WithMessage("{PropertyName} must be at least of length {MaxLength}");
            RuleFor(request => request.Score)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} is out of range")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} is out of range");
            RuleFor(request => request.NegativeScore)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} is out of range")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} is out of range");
            RuleForEach(request => request.Options).ChildRules(options =>
            {
                options.RuleFor(option => option.Text)
                    .NotEmpty().WithMessage("{PropertyName} is required");
            });
            RuleFor(request => request.Options)
                .Must((obj, options, context) =>
                {
                    if (obj.Type == Transport.Shared.Models.QuestionType.MultipleChoice)
                        return obj.Options.Where(o => o.Answer == true).Count() >= 1;

                    return true;
                }).WithMessage("{PropertyName} should have at least one answer");
            RuleFor(request => request.Options)
                .Must((obj, options, context) =>
                {
                    if (obj.Type == Transport.Shared.Models.QuestionType.SingleChoice)
                        return obj.Options.Where(o => o.Answer == true).Count() == 1;

                    return true;
                }).WithMessage("{PropertyName} should have only one answer");
        }
    }
}