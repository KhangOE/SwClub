namespace SwClub.Web.Responses.Base
{
    using System.Net;

    public class ExceptionRS
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public string ExceptionMessage { get; }

        public ExceptionRS(HttpStatusCode statusCode, string errorCode, string message, string exceptionMessage)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Message = message;
            ExceptionMessage = exceptionMessage;
        }
    }
}
