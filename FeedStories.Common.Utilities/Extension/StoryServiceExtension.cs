using FeedStories.Common.Utilities.Interface;
using FeedStories.Common.Utilities.Policies;
using FeedStories.Common.Utilities.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FeedStories.Common.Utilities.Extension
{
    /// <summary>
    /// StoryServiceExtension class is used to register story service in dependency injection container which delas with external service
    /// </summary>
    public static class StoryServiceExtension
    {
        public static IServiceCollection AddStoryService(this IServiceCollection services, string uri)
        {
            services.AddMemoryCache();

            services.AddHttpClient<IStoryService, StoryService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            })
            .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
            .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy())
            .AddPolicyHandler(HttpPolicies.GetTimeoutPolicy());

            services.AddSingleton<ILogger<StoryService>>(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger<StoryService>();
            });
            return services;
        }
    }
}