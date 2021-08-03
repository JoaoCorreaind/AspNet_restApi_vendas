using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DomainInterfaces
{
    public interface IVendedorRepository
    {
        Task<List<Vendedor>> getAllAsync();
        Task<Vendedor> getByIdAsync(string id);
        Task<dynamic> storeAsync(Vendedor vendedor);
        Task<dynamic> updateAsync(Vendedor vendedor);
        Task<dynamic> deleteAsync(string id);
    }
}
