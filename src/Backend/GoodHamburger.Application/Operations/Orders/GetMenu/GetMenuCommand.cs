using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.GetMenu
{
    public class GetMenuCommand : IRequest<Result<MenuDto>>
    {
    }
}
