using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private IQueryable<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.Lines)
                                .ThenInclude(o => o.Toy)
                                .AsNoTracking();
        }

        public  async Task<List<Order>> Orders(bool status)
        {
            return await GetOrders().Where(o => o.Shipped == status)
                          .ToListAsync();
        }

        public async Task<List<Order>> Orders()
        {
            return await GetOrders().AsNoTracking()
                         .ToListAsync();
        }

        public async Task<bool> SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Toy));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Order> FindByIdAsync(int id)
        {
           return await _context.Orders.Where(o => o.OrderID == id).FirstOrDefaultAsync();
        }
    }
}
