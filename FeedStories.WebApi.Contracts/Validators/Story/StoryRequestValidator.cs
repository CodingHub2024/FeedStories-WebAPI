using FeedStories.WebApi.Contracts.Request;
using FluentValidation;

namespace FeedStories.WebApi.Contracts.Validators
{
    public class StoryRequestValidator : AbstractValidator<StoryRequest>
    {
        public StoryRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0)
                                   .WithMessage("PageNumber must be greater than or equal to 0");

            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1)
                                     .WithMessage("PageSize must be greater than or equal to 1");
        }
    }
}