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

        public async Task<Toy> FindByIdAsync(int id)
        {
            return await _context.Toys.AsNoTracking().Include(t => t.Category)
                    .Include(t=>t.CartLines).FirstOrDefaultAsync(t => t.ToyId == id);
        }

        public async Task AddOrUpdate(Toy toy)
        {
            if (toy.ToyId == 0)
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

        public IQueryable<Toy> GetAll()
        {
            return _context.Toys.AsQueryable().AsNoTracking();
        }

        public async Task<Toy> DeleteById(int toyId)
        {
            var entry = _context.Toys.FirstOrDefault(t => t.ToyId == toyId);

            if (entry != null)
            {
                _context.Toys.Remove(entry);
                await _context.SaveChangesAsync();
            }

            return entry;
        }

        public async Task<Toy> FindByIdForSerializeAsync(int id)
        {
            return await _context.Toys.FirstOrDefaultAsync(t => t.ToyId == id);
        }
    }
}