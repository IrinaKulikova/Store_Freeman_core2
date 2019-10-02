using System.Collections.Generic;

namespace Store.Models
{
    public class Toy
    {
        public int ToyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public ICollection<CartLine> CartLines { get; set; }
    }
}