using Store.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IToyRepository
    {
        IQueryable<Toy> GetAll();
        Task<Toy> FindByIdAsync(int id);
        Task<Toy> FindByIdForSerializeAsync(int id);
        Task AddOrUpdate(Toy toy);
        Task<Toy> DeleteById(int toyId);
    }
}