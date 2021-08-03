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
using WebApplication1.Repositores;
using WebApplication1.DomainInterfaces;

namespace WebApplication1.Controllers
{
    [Route("v1/categorias")]
    public class CategoriasController : Controller
    {
        private readonly CategoriaContext _categoriaContext;
        private readonly ICategoriaRepository _repository;

        public CategoriasController(IOptions<ConfigDb> opcoes, ICategoriaRepository repository)
        {
            _categoriaContext = new CategoriaContext(opcoes);
            _repository = repository;
        }
       

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> index()
        {
            var categorias = await _repository.getAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> find(string id)
        {
            var categoria = await _repository.getByIdAsync(id);
            if (categoria != null)
            {
                return categoria;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> store(

            [FromBody] Categoria categoria
            )
        {
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            await _repository.storeAsync(categoria);
            return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Categoria = categoria });
        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> update(
           [FromBody] Categoria categoria
           )
        {
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            try
            {
                var response = await _repository.updateAsync(categoria);
                return response;
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
                var response = await _repository.deleteAsync(id);
                return response;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message, trace = e.StackTrace });

            }


        }
    }
}
