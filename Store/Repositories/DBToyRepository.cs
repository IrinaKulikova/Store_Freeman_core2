using System.Linq;
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

        public IQueryable<Toy> Toys => _context.Toys;
    }
}
