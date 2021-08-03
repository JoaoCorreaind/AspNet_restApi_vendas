using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainInterfaces;
using WebApplication1.Models;
using MongoDB.Driver;
namespace WebApplication1.Repositores
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly ProductContext _context;
        public ProdutoRepository(IOptions<ConfigDb> opcoes)
        {
            this._context = new ProductContext(opcoes);
        }
        public async Task<List<Product>> getAllAsync()
        {
            var response = await _context.Produtos.Find(x => true).ToListAsync();
            return response;
        }

        public async Task<Product> getByIdAsync(string id)
        {
            var response = await _context.Produtos.Find(x => x.Id == id).FirstOrDefaultAsync();
            return response;
        }

        public async Task<dynamic> storeAsync(Product produto)
        {
            try
            {
                produto.setDate();
                await _context.Produtos.InsertOneAsync(produto);
                return true;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<dynamic> updateAsync(Product produto)
        {
            try
            {
                var response = await _context.Produtos.FindOneAndReplaceAsync(x => x.Id == produto.Id, produto);
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
                var response = await _context.Produtos.FindOneAndDeleteAsync(x => x.Id == id);
                return response;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
    }
}
