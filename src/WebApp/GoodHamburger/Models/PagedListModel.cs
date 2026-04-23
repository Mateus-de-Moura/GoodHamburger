namespace GoodHamburger.Models
{
    public class PagedListModel<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<T> CurrentItems { get; set; } = [];
    }
}
