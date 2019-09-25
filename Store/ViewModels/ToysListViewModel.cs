using Store.Models;
using System.Collections.Generic;

namespace Store.ViewModels
{
    public class ToysListViewModel
    {
        public IEnumerable<Toy> Toys { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
