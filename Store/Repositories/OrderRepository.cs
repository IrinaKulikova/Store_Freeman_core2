using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.DataBaseContext;
using Store.Repositories.Interfaces;

namespace Store.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private StoreContext _context;
        
        public OrderRepository(StoreContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Order> Orders =>
             _context.Orders.Include(o => o.Lines).ThenInclude(o => o.Toy);


        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Toy));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
