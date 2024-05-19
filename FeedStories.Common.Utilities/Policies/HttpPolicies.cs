using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Caching;
using Polly.Caching.Memory;

namespace FeedStories.Common.Utilities.Policies
{
    /// <summary>
    /// Policies class is used to define fault tolerance policies for the httpclient
    /// </summary>
    public static class HttpPolicies
    {
        /// <summary>
        /// Retry service policy
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// Circut breaker policy for service
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromMinutes(10));
        }

        /// <summary>
        /// Timeout policy for service
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(15);
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCachPolicy(IServiceCollection services)
        {
            var memoryCache = services.BuildServiceProvider().GetRequiredService<IMemoryCache>();
            var cacheProvider = new MemoryCacheProvider(memoryCache);

            return Policy.CacheAsync<HttpResponseMessage>(
                cacheProvider.AsyncFor<HttpResponseMessage>(),
                TimeSpan.FromSeconds(30) // set short expiration time for live data
            );
        }
    }
}
