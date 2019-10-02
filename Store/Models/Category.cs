using System.Collections.Generic;

namespace Store.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Toy> Toys { get; set; }
    }
}
