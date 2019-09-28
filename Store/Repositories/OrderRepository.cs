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
        #region private fields

        private StoreContext _context;

        #endregion

        #region ctor

        public OrderRepository(StoreContext ctx)
        {
            _context = ctx;
        }

        #endregion

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
            return await GetOrders().ToListAsync();
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
           return await _context.Orders.AsNoTracking()
                                .FirstOrDefaultAsync(o => o.OrderID == id);
        }
    }
}
