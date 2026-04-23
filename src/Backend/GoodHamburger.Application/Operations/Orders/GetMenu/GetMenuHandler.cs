using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Operations.Orders.Common;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetMenu
{
    public class GetMenuHandler(IProductRepository productRepository) : IRequestHandler<GetMenuCommand, Result<MenuDto>>
    {
        public async Task<Result<MenuDto>> Handle(GetMenuCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllProducts(cancellationToken);

            if (!products.IsSuccess)
                return Result<MenuDto>.Error(string.Join(" | ", products.Errors));

            var dto = new MenuDto();

            foreach (var product in products.Value.OrderBy(p => p.Name))
            {
                var item = new MenuItemDto
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Category = product.Category?.Description ?? string.Empty,
                    Price = product.Price
                };

                if (!OrderProductClassifier.TryGetType(product, out var itemType))
                {
                    dto.Others.Add(item);
                    continue;
                }

                if (itemType == MenuItemType.Sandwich)
                {
                    dto.Sandwiches.Add(item);
                    continue;
                }

                if (itemType == MenuItemType.Side)
                {
                    dto.Sides.Add(item);
                    continue;
                }

                dto.Drinks.Add(item);
            }

            return Result.Success(dto);
        }
    }
}
