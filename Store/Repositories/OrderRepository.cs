﻿using System.Collections.Generic;
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

        public OrderRepository(StoreContext cotext)
        {
            _context = cotext;
        }

        #endregion

        private IQueryable<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.Lines).ThenInclude(o => o.Toy)
                    .AsNoTracking();
        }

        public IQueryable<Order> Orders(bool status)
        {
            return GetOrders().Where(o => o.Shipped == status);
        }

        public IQueryable<Order> Orders()
        {
            return GetOrders();
        }

        public async Task SaveOrUpdate(Order order)
        {
            if (order.Lines != null || order.OrderID == 0)
            {
                _context.AttachRange(order.Lines.Select(l => l.Toy));
            }

            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }
            else
            {
                _context.Attach(order);
                _context.Entry(order).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await _context.Orders.AsNoTracking()
                    .FirstOrDefaultAsync(o => o.OrderID == id);
        }
    }
}
