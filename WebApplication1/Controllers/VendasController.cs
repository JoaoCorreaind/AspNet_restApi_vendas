using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models.venda;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("v1/vendas")]
    public class VendasController : Controller
    {
        private readonly VendaContext _vendaContext;


        public VendasController(IOptions<ConfigDb> opcoes)
        {
            _vendaContext = new VendaContext(opcoes);
        }

        [HttpGet]
        public async Task<ActionResult<List<Venda>>> index()
        {

            var vendas = await _vendaContext.Vendas.Find(x => true).ToListAsync();

            return vendas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> find(string id)
        {
            var venda = await _vendaContext.Vendas.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (venda != null)
            {
                return venda;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Categoria não encontrada", complemento = "verifique o criterio de busca", status = "400" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Venda>> store(

            [FromBody] Venda venda
            )
        {
            venda.setDate();
            foreach (Product produto  in venda.produtos)
            {
                venda.valor += produto.valorVenda;
            }
            try
            {
                await _vendaContext.Vendas.InsertOneAsync(venda);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Venda = venda });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao criar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<Venda>> update(
           [FromBody] Venda venda
           )
        {
            foreach (Product produto in venda.produtos)
            {
                venda.valor += produto.valorVenda;
            }
            try
            {
                var response = await _vendaContext.Vendas.ReplaceOneAsync<Venda>(x => x.Id == venda.Id, venda);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao atualizar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Venda>> Delete(string id)
        {
            try
            {
                var response = await _vendaContext.Vendas.FindOneAndDeleteAsync<Venda>(x => x.Id == id);

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
