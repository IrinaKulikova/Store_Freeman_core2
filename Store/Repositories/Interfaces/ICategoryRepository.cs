using Store.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories();
        Task<Category> FindByName(string name);
        Task<Category> FindById(int id);
    }
}
