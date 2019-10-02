using System.Collections.Generic;
using System.Linq;

namespace Store.Models
{
    public class Cart
    {
        #region private Fields

        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        #endregion

        public virtual void AddItem(Toy toy, int quantity)
        {
            var line = _lineCollection
                        .Where(t => t.Toy.ToyId == toy.ToyId)
                        .FirstOrDefault();

            if (line == null)
            {
                _lineCollection.Add(new CartLine()
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

        public virtual void RemoveLine(int toyId)
        {
            _lineCollection.RemoveAll(l => l.Toy.ToyId == toyId);
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
