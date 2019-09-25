using Store.Models;
using System.Linq;

namespace Store.Repositories.Interfaces
{
    public interface IToyRepository
    {
        IQueryable<Toy> Toys { get; }
    }
}