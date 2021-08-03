using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainInterfaces;
using WebApplication1.Models.venda;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace WebApplication1.Repositores
{
    public class VendaRepository : IVendaRepository
    {

        private readonly VendaContext _context;
        public VendaRepository(IOptions<ConfigDb> opcoes)
        {
            this._context = new VendaContext(opcoes);
        }
        public async Task<List<Venda>> getAllAsync()
        {
            var response = await _context.Vendas.Find(x => true).ToListAsync();
            return response;
        }

        public async Task<Venda> getByIdAsync(string id)
        {
            var response = await _context.Vendas.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<dynamic> storeAsync(Venda venda)
        {
            try
            {
                venda.setDate();
                foreach (var produto in venda.Produtos)
                {
                    venda.Valor += produto.ValorVenda;
                }
                await _context.Vendas.InsertOneAsync(venda);
                return true;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
       
        public async Task<dynamic> updateAsync(Venda venda)
        {
            try
            {
                foreach (var produto in venda.Produtos)
                {
                    venda.Valor += produto.ValorVenda;
                }
                await _context.Vendas.FindOneAndReplaceAsync(x => x.Id == venda.Id, venda);
                return true;
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
                await _context.Vendas.FindOneAndDeleteAsync(x => x.Id == id);
                return true;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        
    }
}
