using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.DataBaseContext;
using Store.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;

        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories()
        {
            return _context.Categories.AsNoTracking();
        }

        public async Task<Category> FindById(int id)
        {
            return await _context.Categories
                          .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category> FindByName(string name)
        {
            return await _context.Categories.Include(t => t.Toys)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
