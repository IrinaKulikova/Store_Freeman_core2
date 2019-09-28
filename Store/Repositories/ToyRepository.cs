using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.DataBaseContext;
using Store.Repositories.Interfaces;

namespace Store.Repositories
{
    public class ToyRepository : IToyRepository
    {
        #region private fields

        private StoreContext _context;

        #endregion

        #region ctor

        public ToyRepository(StoreContext context)
        {
            _context = context;
        }

        #endregion

        public List<string> Catrgories => _context.Toys.Select(t => t.Category)
                                                   .AsNoTracking()
                                                   .Distinct().ToList();

        public async Task<Toy> FindByIdAsync(int id)
        {
            return await _context.Toys.AsNoTracking()
                                 .FirstOrDefaultAsync(t => t.ToyID == id);
        }

        public IQueryable<Toy> Toys()
        {
            return _context.Toys.AsQueryable()
                                .AsNoTracking();
        }
    }
}
