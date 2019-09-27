using Store.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repositories.Interfaces
{
    public interface IToyRepository
    {
        Task<List<Toy>> Toys();
        Task<List<Toy>> Toys(Func<Toy, bool> predicate);
        Task<Toy> FindByIdAsync(int id);
        List<string> Catrgories { get; }
    }
}