namespace GoodHamburger.Application.Dtos
{
    public class MenuDto
    {
        public List<MenuItemDto> Sandwiches { get; set; } = [];
        public List<MenuItemDto> Sides { get; set; } = [];
        public List<MenuItemDto> Drinks { get; set; } = [];
        public List<MenuItemDto> Others { get; set; } = [];
    }
}
