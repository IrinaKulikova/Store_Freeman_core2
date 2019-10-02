using Store.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders(bool status);
        IQueryable<Order> Orders();
        Task<Order> FindByIdAsync(int id);
        Task SaveOrUpdate(Order order);
    }
}
