namespace GoodHamburger.Domain.Result
{
    public class PagedList<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> CurrentItems { get; set; }

        public PagedList(int pageNumber, int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}
