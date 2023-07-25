namespace SwClub.DataTransferObjects.Base
{
    public class BaseResponseDTO<T>
    {
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
    }
}
