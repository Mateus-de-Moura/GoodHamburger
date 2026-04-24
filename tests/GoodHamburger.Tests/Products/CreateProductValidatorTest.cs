using GoodHamburger.Application.Operations.Products.CreateProducts;
using GoodHamburger.Tests.Products.Build;
using Shouldly;

namespace GoodHamburger.Tests.Products
{
    public class CreateProductValidatorTest
    {
        private readonly CreateProducValidator _validator = new();
        public static IEnumerable<object[]> InvalidFields()
        {
            var random = new Random();

            yield return new object[] { "", random.Next(1, 100), "Description" };
            yield return new object[] { "Hambúrguer", 0m, "Price" };
            yield return new object[] { "Hambúrguer", -10m, "Price" };
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Nome_Estiver_Vazio()
        {
            var command = new CreateCommandBuilder()
               .WithName("")
               .WithDescription("Hambúrguer artesanal")
               .WithPrice(29.90m)
               .WithCategory(Guid.NewGuid())
               .Build();

            var result = _validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBeGreaterThan(0);
            result.Errors.Any(x => x.PropertyName == "Name").ShouldBeTrue();
            result.Errors.First(x => x.PropertyName == "Name")
                .ErrorMessage
                .ShouldBe("O nome do produto precisa ser preenchido.");
        }

        [Theory]
        [MemberData(nameof(InvalidFields))]
        public void Deve_Retornar_Erro_Quando_Campos_Invalidos(string description, decimal price, string campoEsperado)
        {
            var command = new CreateCommandBuilder()
                .WithName("X-Burger")
                .WithDescription(description)
                .WithPrice(price)
                .WithCategory(Guid.NewGuid())
                .Build();

            var result = _validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.Any(x => x.PropertyName == campoEsperado)
                .ShouldBeTrue();
        }


        [Fact]
        public void Deve_Retornar_Sucesso_Quando_Dados_Forem_Validos()
        {
            var command = new CreateCommandBuilder()
                .WithName("X-Burger")
                .WithDescription("Hambúrguer artesanal")
                .WithPrice(29.90m)
                .WithCategory(Guid.NewGuid())
                .Build();

            var result = _validator.Validate(command);

            result.IsValid.ShouldBeTrue();
            result.Errors.Count.ShouldBe(0);
        }
    }
}