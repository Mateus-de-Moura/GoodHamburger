namespace GoodHamburger.Models
{
    public class MenuModel
    {
        public List<MenuItemModel> Sandwiches { get; set; } = [];
        public List<MenuItemModel> Sides { get; set; } = [];
        public List<MenuItemModel> Drinks { get; set; } = [];
        public List<MenuItemModel> Others { get; set; } = [];
    }
}
