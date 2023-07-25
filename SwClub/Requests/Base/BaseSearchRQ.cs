namespace SwClub.Web.Requests.Base
{
    using SwClub.Web.Requests.Base;
    using SwClub.Common.Constants;

    public class BaseSearchRQ : IPaginator, ISorting
    {
        public BaseSearchRQ()
        {
            this.Start = GlobalConstant.PageConfig.Start;
            this.Length = GlobalConstant.PageConfig.Length;
        }

        /// <summary>
        /// Start page.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Sort column.
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Order by asc or desc.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Keyword.
        /// </summary>
        public string Keyword { get; set; }
    }
}
