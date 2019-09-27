using System.Collections.Generic;
using System.Linq;

namespace Store.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Toy toy, int quantity)
        {
            CartLine line = _lineCollection
                    .Where(t => t.Toy.ToyID == toy.ToyID)
                    .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Toy = toy,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Toy toy)
        {
            _lineCollection.RemoveAll(l => l.Toy.ToyID == toy.ToyID);
        }

        public virtual decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Toy.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            _lineCollection.Clear();
        }

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }
}
