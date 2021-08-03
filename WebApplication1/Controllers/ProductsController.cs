using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Net;
using WebApplication1.DomainInterfaces;

namespace WebApplication1.Controllers
{

    [Route("v1/produtos")]
    public class ProductsController : Controller
    {

        private readonly IProdutoRepository _repository;


        public ProductsController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> index()
        {
            var produtos = await _repository.getAllAsync();
            return produtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> find(string id)
        {
            var produto = await _repository.getByIdAsync(id);
            if(produto != null)
            {
                return produto;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> store(
          
            [FromBody] Product produto
            )
        {
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            try
            {
                var response = await _repository.storeAsync(produto);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Produto = produto, Response = response });
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
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            try
            {
                var response = await _repository.updateAsync(produto);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now, New = response });
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
                var response = await _repository.deleteAsync(id);

                return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, Response = response });
            }catch(Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message , trace = e.StackTrace });

            }


        }
    }
}
