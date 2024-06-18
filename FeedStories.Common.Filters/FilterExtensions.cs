using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FeedStories.WebApi.Contracts.Validators;
using FluentValidation;
using FeedStories.WebApi.Contracts.Request;

namespace FeedStories.Common.Filters
{
    /// <summary>
    /// FilterExtensions is used to register fluent validation
    /// </summary>
    public static class FilterExtensions
    {
        public static void ValidateModels(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<StoryRequestValidator>());
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationModelFilter>();
            });
        }
    }
}