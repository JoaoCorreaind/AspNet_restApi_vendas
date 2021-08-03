using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.fornecedor;

namespace WebApplication1.DomainInterfaces
{
    public interface IFornecedorRepository
    {
        Task<List<Fornecedor>> getAllAsync();
        Task<Fornecedor> getByIdAsync(string id);
        Task<dynamic> storeAsync(Fornecedor fornecedor);
        Task<dynamic> updateAsync(Fornecedor fornecedor);
        Task<dynamic> deleteAsync(string id);
    }
}
