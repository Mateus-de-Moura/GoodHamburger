using GoodHamburger.Application.Operations.Products.CreateProducts;

namespace GoodHamburger.Tests.Products.Build
{
    public class CreateCommandBuilder
    {
        private readonly CreateProductsCommand _command;

        public CreateCommandBuilder()
        {
            _command = new CreateProductsCommand();            
        }

        public CreateCommandBuilder WithName(string name)
        {
            _command.Name = name;
            return this;
        }

        public CreateCommandBuilder WithDescription(string description)
        {
            _command.Description = description;
            return this;
        }

        public CreateCommandBuilder WithPrice(decimal price)
        {
            _command.Price = price;
            return this;
        }

        public CreateCommandBuilder WithCategory(Guid categoryId)
        {
            _command.CategoryId = categoryId;
            return this;
        }

        public CreateProductsCommand Build()
        {
            return _command;
        }
    }
}