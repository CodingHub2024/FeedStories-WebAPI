using Polly;
using Polly.Extensions.Http;

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

    }
}
