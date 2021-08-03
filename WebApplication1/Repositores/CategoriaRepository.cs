using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainInterfaces;
using WebApplication1.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models.venda;

namespace WebApplication1.Repositores
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly CategoriaContext _context;
        public CategoriaRepository(IOptions<ConfigDb> opcoes)
        {
            this._context = new CategoriaContext(opcoes);
        }

       

        public async Task<List<Categoria>> getAllAsync()
        {
            return await _context.Categoria.Find(x => true).ToListAsync();
        }

        public async Task<Categoria> getByIdAsync(string id)
        {
            return await _context.Categoria.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<dynamic> storeAsync(Categoria categoria)
        {
            try
            {
                categoria.created_at = DateTime.Now;
                await _context.Categoria.InsertOneAsync(categoria);
                return true;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
           
        }

        public async Task<dynamic> updateAsync(Categoria categoria)
        {
            var response = await _context.Categoria.FindOneAndReplaceAsync<Categoria>(x => x.Id == categoria.Id, categoria);
            return response;

        }
        public async Task<dynamic> deleteAsync(string id)
        {
            var response = await _context.Categoria.FindOneAndDeleteAsync<Categoria>(x => x.Id == id);
            return response;
        }
    }
}
