namespace SwClub.Web.Responses.Base
{
    using System.Collections.Generic;

    public class BaseTableRS<T>
         where T : class
    {
        public BaseTableRS(bool success = true)
        {
            this.Success = success;
        }

        public List<T> Items { get; set; }

        public int Total { get; set; }

        public bool Success { get; set; }

        public string ErrorCode { get; set; }

        public string Message { get; set; }
    }
}
