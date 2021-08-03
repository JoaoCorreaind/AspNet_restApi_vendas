using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DomainInterfaces
{
    public interface IProdutoRepository
    {
        Task<List<Product>> getAllAsync();
        Task<Product> getByIdAsync(string id);
        Task<dynamic> storeAsync(Product produto);
        Task<dynamic> updateAsync(Product produto);
        Task<dynamic> deleteAsync(string id);
    }
}
