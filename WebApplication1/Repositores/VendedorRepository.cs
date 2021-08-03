using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainInterfaces;
using WebApplication1.Models;
using MongoDB.Driver;
using WebApplication1.Models.fornecedor;
using Microsoft.Extensions.Options;

namespace WebApplication1.Repositores
{
    public class VendedorRepository : IVendedorRepository
    {

        private readonly VendedorContext _context;
        public VendedorRepository(IOptions<ConfigDb> opcoes)
        {
            this._context = new VendedorContext(opcoes);
        }
        public async Task<List<Vendedor>> getAllAsync()
        {
            var response = await _context.Vendedores.Find(x => true).ToListAsync();
            return response;
        }

        public async Task<Vendedor> getByIdAsync(string id)
        {
            var response = await _context.Vendedores.Find(x => x.Id == id).FirstOrDefaultAsync();
            return response;
        }

        public async Task<dynamic> storeAsync(Vendedor vendedor)
        {
            try
            {
                vendedor.created_at = DateTime.Now;
                await _context.Vendedores.InsertOneAsync(vendedor);
                return true;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<dynamic> updateAsync(Vendedor vendedor)
        {
            try
            {
                var response = await _context.Vendedores.FindOneAndReplaceAsync(x => x.Id == vendedor.Id, vendedor);
                return response;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
        public async Task<dynamic> deleteAsync(string id)
        {
            try
            {
                var response = await _context.Vendedores.FindOneAndDeleteAsync(x => x.Id == id);
                return response;
            }
            catch (Exception e)
            {

                return e;
            }
        }
    }
}
