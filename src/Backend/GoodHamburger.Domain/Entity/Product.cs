using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
         public byte[] ImageBytes { get; set; } = [];
    }
}
