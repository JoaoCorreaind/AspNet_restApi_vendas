using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.venda;

namespace WebApplication1.DomainInterfaces
{
    public interface IVendaRepository
    {
        Task<List<Venda>> getAllAsync();
        Task<Venda> getByIdAsync(string id);
        Task<dynamic> storeAsync(Venda produto);
        Task<dynamic> updateAsync(Venda produto);
        Task<dynamic> deleteAsync(string id);
    }
}
