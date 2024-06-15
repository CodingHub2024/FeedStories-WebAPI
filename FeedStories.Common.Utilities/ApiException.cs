using FeedStories.Common.Utilities.Type;

namespace FeedStories.Common.Utilities
{
    public class ApiException : Exception
    {
        public ErrorCodes ErrorCode { get; private set; }
        public string? ErrorMessage { get; private set; }

        public ApiException() : base() { }

        public ApiException(ErrorCodes errorCodes, string? errorMessage = null) : base(errorMessage)
        {
            ErrorCode = errorCodes;
            ErrorMessage = errorMessage;
        }

        public override string Message
        {
            get 
            { 
                return $"{ErrorCode} : {ErrorMessage}";
            }
        }
    }
}
