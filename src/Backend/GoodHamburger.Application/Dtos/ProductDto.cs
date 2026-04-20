using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
         public byte[] ImageBytes { get; set; } = [];
    }
}
