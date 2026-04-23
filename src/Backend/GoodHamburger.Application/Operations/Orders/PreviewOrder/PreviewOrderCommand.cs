using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;

namespace GoodHamburger.Application.Operations.Orders.PreviewOrder
{
    public class PreviewOrderCommand : IRequest<Result<OrderPreviewDto>>
    {
        public List<Guid> ProductIds { get; set; } = [];
    }
}
