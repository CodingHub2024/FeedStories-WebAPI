using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace FeedStories.Common.Filters
{
    public static class FilterExtensions
    {
        public static void ValidateModels(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationModelFilter>();
            });
        }
        
    }
}






