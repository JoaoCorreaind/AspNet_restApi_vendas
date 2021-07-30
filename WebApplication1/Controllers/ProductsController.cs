using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Net;

namespace WebApplication1.Controllers
{

    [Route("v1/produtos")]
    public class ProductsController : Controller
    {

        private readonly ProductContext _productContext;


        public ProductsController(IOptions<ConfigDb> opcoes)
        {
            _productContext = new ProductContext(opcoes);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> index()
        {
            var produtos = await _productContext.Produtos.Find(x => true).ToListAsync();
            return produtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> find(string id)
        {
            var produto = await _productContext.Produtos.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(produto != null)
            {
                return produto;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Produto não encontrado", complemento = "verifique o criterio de busca", status = "400" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> store(
          
            [FromBody] Product produto
            )
        {
            produto.setDate();
            try
            {
                await _productContext.Produtos.InsertOneAsync(produto);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Produto = produto });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao criar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<Product>> update(
           [FromBody] Product produto
           )
        {

            try
            {
                var response = await _productContext.Produtos.ReplaceOneAsync<Product>(x => x.Id == produto.Id, produto);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now });
            }catch(Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao atualizar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(string id)
        {
            try
            {
                var response = await _productContext.Produtos.FindOneAndDeleteAsync<Product>(x => x.Id == id);

                return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, DeletedProdut = response });
            }catch(Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message , trace = e.StackTrace });

            }


        }

        //public async Task<IActionResult> index()
        //{
        //    var response = await _productContext.Produtos.Find(x => true).ToListAsync();
        //    return (IActionResult)response;

        //}

        //[HttpGet]
        //public IActionResult NovoProd()
        //{
        //    return View();
        //}


        //public async Task<IActionResult> store(Product produto)
        //{
        //    produto.created_at = DateTime.Now;
        //    await _productContext.Produtos.InsertOneAsync(produto);
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpGet("Products/delete/{id}")]
        //public async  Task<IActionResult> delete(int id)
        //{
        //    var deleteFilter = Builders<Product>.Filter.Eq("Id", id);
        //    await _productContext.Produtos.DeleteOneAsync(deleteFilter);
        //    return RedirectToAction(nameof(Index));

        //}
    }
}
