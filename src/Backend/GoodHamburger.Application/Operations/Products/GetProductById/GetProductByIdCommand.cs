using Ardalis.Result;
using GoodHamburger.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Operations.Products.GetById
{
    public class GetProductByIdCommand : IRequest<Result<ProductDto>>
    {
        public Guid Id { get; set; }
    }
}
