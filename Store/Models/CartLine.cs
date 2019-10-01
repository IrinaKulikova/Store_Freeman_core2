using Store.DTOModels;

namespace Store.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Toy Toy { get; set; }
        public int Quantity { get; set; }
    }
}
