using Microsoft.Extensions.DependencyInjection;

namespace FeedStories.Common.Utilities.Infrastructure
{
    /// <summary>
    /// HttpHelperExtension class is used to register http client services in the dependency injection container
    /// </summary>
    public static class HttpHelperExtension
    {
        public static IServiceCollection AddHttpHelper(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpHelper, HttpHelper>()
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy())
            .AddPolicyHandler(PollyPolicies.GetTimeoutPolicy());

            return services;
        }
    }
}