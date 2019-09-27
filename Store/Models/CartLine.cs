using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Toy Toy { get; set; }
        public int Quantity { get; set; }
    }
}
