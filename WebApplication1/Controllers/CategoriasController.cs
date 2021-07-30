using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Net;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using FluentValidation.Results;
using System.Text;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace WebApplication1.Controllers
{
    [Route("v1/categorias")]
    public class CategoriasController : Controller
    {
        private readonly CategoriaContext _categoriaContext;


        public CategoriasController(IOptions<ConfigDb> opcoes)
        {
            _categoriaContext = new CategoriaContext(opcoes);
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> index()
        {

            var categorias = await _categoriaContext.Categoria.Find(x => true).ToListAsync();

            return categorias;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> find(string id)
        {
            var categoria = await _categoriaContext.Categoria.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (categoria != null)
            {
                return categoria;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Categoria não encontrada", complemento = "verifique o criterio de busca", status = "400" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> store(

            [FromBody] Categoria categoria
            )
        {

            categoria.created_at = DateTime.Now;
            try
            {
                await _categoriaContext.Categoria.InsertOneAsync(categoria);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Categoria = categoria });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao criar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<Product>> update(
           [FromBody] Categoria categoria
           )
        {

            try
            {
                var response = await _categoriaContext.Categoria.ReplaceOneAsync<Categoria>(x => x.Id == categoria.Id, categoria);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao atualizar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(string id)
        {
            try
            {
                var response = await _categoriaContext.Categoria.FindOneAndDeleteAsync<Categoria>(x => x.Id == id);

                return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, DeletedCategoria = response });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message, trace = e.StackTrace });

            }


        }
    }
}
