using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Caching;
using Microsoft.Extensions.Configuration;
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
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryNumber,int retryTimeSpan)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(retryNumber, retryAttempt => TimeSpan.FromSeconds(retryTimeSpan));
        }

        /// <summary>
        /// Circut breaker policy for service
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int circuitBreakerNumber,int circuitBreakerWaitTimeSpan)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(circuitBreakerNumber, TimeSpan.FromMinutes(circuitBreakerWaitTimeSpan));
        }

        /// <summary>
        /// Timeout policy for service
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(int timeout)
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(timeout);
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCachPolicy(IServiceCollection services, IConfiguration configuration)
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
