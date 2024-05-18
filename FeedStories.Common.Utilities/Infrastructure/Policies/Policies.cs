using Polly;
using Polly.Extensions.Http;

namespace FeedStories.Common.Utilities.Infrastructure
{
    /// <summary>
    /// PollyPolicies class is used to define fault tolerance policies for the httpclient
    /// </summary>
    public static class PollyPolicies
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
    }
}
