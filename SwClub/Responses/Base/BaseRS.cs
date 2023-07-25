namespace SwClub.Web.Responses.Base
{
    public class BaseRS<T>
    {
        public BaseRS(bool success = true)
        {
            this.Success = success;
        }

        /// <summary>
        /// Success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data result.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Execution time if any.
        /// </summary>
        public string ExecutedTime { get; set; }
    }
}
