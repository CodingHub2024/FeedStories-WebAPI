using FeedStories.WebApi.Contracts.Request;
using FluentValidation;

namespace FeedStories.WebApi.Contracts.Validators
{
    public class StoryDetailRequestValidator : AbstractValidator<StoryDetailRequest>
    {
        public StoryDetailRequestValidator() 
        {
            RuleFor(x => x.StoryId).GreaterThanOrEqualTo(1)
                .WithMessage("StoryId must be greater than or equal to 1"); ;
        }
    }
}