namespace SwClub.DataTransferObjects.Base
{
    public class BasePagingDTO<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public T Items { get; set; }
    }
}
