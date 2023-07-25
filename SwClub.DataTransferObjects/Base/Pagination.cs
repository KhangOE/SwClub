namespace SwClub.DataTransferObjects.Base
{
    using SwClub.Common.Constants;

    public class Pagination<T> where T : class, new()
    {
        public Pagination(IQueryable<T> iQuery)
        {
            this.Query = iQuery;
            this.ExecutePaginate();
        }

        public Pagination(IQueryable<T> iQuery, int pageNumber = GlobalConstant.PageConfig.Start, int pageSize = GlobalConstant.PageConfig.Length)
        {
            this.Query = iQuery;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.ExecutePaginate();
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public List<T> Items { get; set; }

        private IQueryable<T> Query { get; set; }

        private void ExecutePaginate()
        {
            if (this.PageNumber <= 0)
            {
                this.PageNumber = GlobalConstant.PageConfig.Start;
            }

            if (this.PageSize <= 0)
            {
                this.PageSize = GlobalConstant.PageConfig.Length;
            }

            if (this.PageSize > GlobalConstant.PageConfig.MaxLength)
            {
                this.PageSize = GlobalConstant.PageConfig.MaxLength;
            }

            this.TotalItems = this.Query.Count();
            this.TotalPages = (int)Math.Ceiling((double)this.TotalItems / this.PageSize);

            this.Items = this.Query.Skip((this.PageNumber - GlobalConstant.PageConfig.Start) * this.PageSize).Take(this.PageSize).ToList();
        }
    }
}
