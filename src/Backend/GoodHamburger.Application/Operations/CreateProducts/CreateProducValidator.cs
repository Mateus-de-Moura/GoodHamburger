using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Operations.CreateProducts
{
    public class CreateProducValidator : AbstractValidator<CreateProductsCommand>
    {
        public CreateProducValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do produto precisa ser preenchido.");

            RuleFor(x => x.Description)
                 .NotEmpty()
                 .WithMessage("A descrição do produto precisa ser preenchida.");

            RuleFor(x => x.Price)
                 .NotEmpty()
                 .GreaterThan(0)
                 .WithMessage("O preço do produto precisa ser maior que zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("A categoria do produto precisa ser preenchida.");
        }
    }
}
