using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace FeedStories.Common.Filters
{
    /// <summary>
    /// FilterExtensions is used to register fluent validation
    /// </summary>
    public static class FilterExtensions
    {
        public static void ValidateModels(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ValidationModelFilter>();
            });
        }
    }
}