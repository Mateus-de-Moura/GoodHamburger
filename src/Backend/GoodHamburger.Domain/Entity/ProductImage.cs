using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entity
{
    public class ProductImage : BaseEntity
    {
        public byte[] ImageBytes { get; set; } = [];
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
