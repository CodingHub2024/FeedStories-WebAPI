using FeedStories.Common.Utilities.Interface;
using FeedStories.Common.Utilities.Policies;
using FeedStories.Common.Utilities.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FeedStories.Common.Utilities.Extension
{
    /// <summary>
    /// StoryServiceExtension class is used to register story service in dependency injection container which delas with external service
    /// </summary>
    public static class StoryServiceExtension
    {
        public static IServiceCollection AddStoryService(this IServiceCollection services, IConfiguration configuration, string uri)
        {
            services.AddMemoryCache();

            services.AddHttpClient<IStoryService, StoryService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            })
            .AddPolicyHandler(HttpPolicies.GetRetryPolicy(configuration.GetValue<int>("HttpPolicy:RetryNumber"), configuration.GetValue<int>("HttpPolicy:RetryTimeSpan")))
            .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy(configuration.GetValue<int>("HttpPolicy:CircuitBreakerNumber"), configuration.GetValue<int>("HttpPolicy:CircuitBreakerWaitTimeSpan")))
            .AddPolicyHandler(HttpPolicies.GetTimeoutPolicy(configuration.GetValue<int>("HttpPolicy:TimeOut")));

            services.AddSingleton<ILogger<StoryService>>(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger<StoryService>();
            });
            return services;
        }
    }
}