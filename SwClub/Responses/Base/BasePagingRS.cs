namespace SwClub.Web.Responses.Base
{
    using System.Collections.Generic;

    public class BasePagingRS<T>
         where T : class
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public List<T> Items { get; set; }
    }
}
