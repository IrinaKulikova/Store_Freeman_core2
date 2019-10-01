using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class Category
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Toy> Toys { get; set; }
    }
}
