namespace SwClub.DataTransferObjects.Base
{
    public class LoggingPropertyDTO
    {
        public string EventName { get; set; }

        public string DataInput { get; set; }

        public string LogType { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public bool? IsSuccess { get; set; }
    }
}
