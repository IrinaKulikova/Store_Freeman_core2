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

        public async Task AddOrUpdate(Toy toy)
        {
            if (toy.ToyID == 0)
            {
                _context.Toys.Add(toy);
            }
            else
            {
                _context.Attach(toy);
                _context.Entry(toy).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public IQueryable<Toy> Toys()
        {
            return _context.Toys.AsQueryable()
                                .AsNoTracking();
        }

        public async Task<Toy> DeleteToy(int toyId)
        {
            var entry = _context.Toys
                                .FirstOrDefault(t => t.ToyID == toyId);
            if (entry != null)
            {
                _context.Toys.Remove(entry);
                await _context.SaveChangesAsync();
            }

            return entry;
        }
    }
}