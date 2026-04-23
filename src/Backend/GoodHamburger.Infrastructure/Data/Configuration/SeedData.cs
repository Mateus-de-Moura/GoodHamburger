namespace GoodHamburger.Infrastructure.Data.Configuration
{
    internal static class SeedData
    {
        internal static readonly DateTime FixedDate = new DateTime(2026, 04, 22, 0, 0, 0, DateTimeKind.Utc);

        internal static readonly Guid SandwichCategoryId = new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a");
        internal static readonly Guid SideCategoryId = new Guid("13cecac6-961b-474a-9843-553a057a6c11");
        internal static readonly Guid DrinkCategoryId = new Guid("3f6577f1-3483-4c64-8cbc-d68f5f0c26e1");

        internal static readonly Guid XBurgerProductId = new Guid("d9dc5f2b-4f9d-4f7a-8e36-03dcee6eced6");
        internal static readonly Guid XEggProductId = new Guid("8f15f4af-8944-43f5-a94d-d3d31b2be7a6");
        internal static readonly Guid XBaconProductId = new Guid("2f8dd6da-2d2e-4f9b-bab0-bd2ebd56d53f");
        internal static readonly Guid FriesProductId = new Guid("e1477ac0-f99a-4b48-a220-3be6d573c2eb");
        internal static readonly Guid SodaProductId = new Guid("e36881ad-5bf0-47c7-afba-2d4b7fdc5f77");

        internal static readonly Guid XBurgerImageId = new Guid("8de4bfd9-92f9-4f0f-b4dc-94e2599559d1");
        internal static readonly Guid XEggImageId = new Guid("a1f83a8d-3ab8-4279-9e0f-38a7c4e67586");
        internal static readonly Guid XBaconImageId = new Guid("32ccdd73-0f6d-4e27-949b-f95f08c5f03c");
        internal static readonly Guid FriesImageId = new Guid("22f4ce90-0763-4b1f-a96b-4df20d457de8");
        internal static readonly Guid SodaImageId = new Guid("cfebf37a-b07f-4adc-8fc3-1f8bfd70f6f0");
    }
}
