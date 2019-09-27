using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.DataBaseContext;
using Store.Repositories.Interfaces;

namespace Store.Repositories
{
    public class DBToyRepository : IToyRepository
    {
        private StoreContext _context;

        public DBToyRepository(StoreContext context)
        {
            _context = context;
        }

        public List<string> Catrgories => _context.Toys.Select(t => t.Category)
                                            .Distinct().ToList();

        public async Task<Toy> FindByIdAsync(int id)
        {
            return await _context.Toys.FirstOrDefaultAsync(t => t.ToyID == id);
        }

        public async Task<List<Toy>> Toys()
        {
            return await _context.Toys.AsNoTracking().ToListAsync();
        }

        public async Task<List<Toy>> Toys(Func<Toy, bool> predicate)
        {
            return await _context.Toys.Where(predicate)
                                      .AsQueryable().ToListAsync();
        }
    }
}
