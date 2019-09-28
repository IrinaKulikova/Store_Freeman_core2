using Store.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IToyRepository
    {
        IQueryable<Toy> Toys();
        Task<Toy> FindByIdAsync(int id);
        List<string> Catrgories { get; }
    }
}