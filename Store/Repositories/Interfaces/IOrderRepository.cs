using Store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> Orders(bool status);
        Task<List<Order>> Orders();
        Task<Order> FindByIdAsync(int id);
        Task SaveOrUpdate(Order order);
    }
}
