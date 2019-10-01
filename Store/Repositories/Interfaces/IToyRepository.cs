using Store.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IToyRepository
    {
        IQueryable<Toy> Toys();
        Task<Toy> FindByIdAsync(int id);
        Task<Toy> FindByIdForSerializeAsync(int id);
        Task AddOrUpdate(Toy toy);
        Task<Toy> DeleteToy(int toyId);
    }
}