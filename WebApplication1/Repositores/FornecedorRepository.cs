using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainInterfaces;
using WebApplication1.Models.fornecedor;
using MongoDB.Driver;
namespace WebApplication1.Repositores
{
    public class FornecedorRepository : IFornecedorRepository
    {

        private readonly FornecedorContext _context;
        public FornecedorRepository(IOptions<ConfigDb> opcoes)
        {
            this._context = new FornecedorContext(opcoes);
        }
        public async Task<List<Fornecedor>> getAllAsync()
        {
            var response = await _context.Fornecedores.Find(x => true).ToListAsync();
            return response;
        }

        public async Task<Fornecedor> getByIdAsync(string id)
        {

            var fornecedores = await _context.Fornecedores.Find(x => x.Id == id).FirstOrDefaultAsync();
            return fornecedores;
        }

        public async Task<dynamic> storeAsync(Fornecedor fornecedor)
        {
            try
            {
                fornecedor.setDate();
                await _context.Fornecedores.InsertOneAsync(fornecedor);
                return true;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<dynamic> updateAsync(Fornecedor fornecedor)
        {
            try
            {
                var response = await _context.Fornecedores.FindOneAndReplaceAsync<Fornecedor>(x => x.Id == fornecedor.Id, fornecedor);
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
                var response = await _context.Fornecedores.FindOneAndDeleteAsync<Fornecedor>(x => x.Id == id);
                return response;
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }
    }
}
