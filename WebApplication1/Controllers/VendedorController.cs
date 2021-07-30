using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace WebApplication1.Controllers
{
    [Route("v1/vendedores")]

    public class VendedorController : Controller
    {
        private readonly VendedorContext _vendedorContext;


        public VendedorController(IOptions<ConfigDb> opcoes)
        {
            _vendedorContext = new VendedorContext(opcoes);
        }

        [HttpGet]
        public async Task<ActionResult<List<Vendedor>>> index()
        {
            var vendedores = await _vendedorContext.Vendedores.Find(x => true).ToListAsync();
            return vendedores;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> find(string id)
        {
            var vendedor = await _vendedorContext.Vendedores.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (vendedor != null)
            {
                return vendedor;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Produto não encontrado", complemento = "verifique o criterio de busca", status = "400" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vendedor>> store(

            [FromBody] Vendedor vendedor
            )
        {
            vendedor.created_at = DateTime.Now;
            try
            {
                await _vendedorContext.Vendedores.InsertOneAsync(vendedor);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Vendedor = vendedor });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao criar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<Vendedor>> update(
           [FromBody] Vendedor vendedor
           )
        {

            try
            {
                var response = await _vendedorContext.Vendedores.ReplaceOneAsync<Vendedor>(x => x.Id == vendedor.Id, vendedor);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao atualizar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendedor>> Delete(string id)
        {
            try
            {
                var response = await _vendedorContext.Vendedores.FindOneAndDeleteAsync<Vendedor>(x => x.Id == id);

                return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, Deleted_Vendedor = response });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message, trace = e.StackTrace });

            }


        }
    }
}
