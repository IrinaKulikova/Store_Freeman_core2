using System;
using System.Runtime.InteropServices;

namespace Store.Models
{
    public class Toy
    {
        public int ToyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }

}