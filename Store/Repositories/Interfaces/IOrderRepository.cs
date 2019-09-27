using Store.Models;
using System.Linq;

namespace Store.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
