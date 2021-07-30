using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.fornecedor;
using MongoDB.Driver;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("v1/fornecedores")]

    public class FornecedorController : Controller
    {    

        private readonly FornecedorContext _fornecedorContext;


        public FornecedorController(IOptions<ConfigDb> opcoes)
        {
            _fornecedorContext = new FornecedorContext(opcoes);
        }
        [HttpGet]
        public async Task<ActionResult<List<Fornecedor>>> index()
        {

            var fornecedores = await _fornecedorContext.Fornecedores.Find(x => true).ToListAsync();

            return fornecedores;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> find(string id)
        {
            var fornecedor = await _fornecedorContext.Fornecedores.Find(x => x.Id == id).FirstOrDefaultAsync(); 
            if(fornecedor != null)
            {
                return fornecedor;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao encontrar", complemento = "Este fornecedor não existe no banco", status = "400" });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> store(
            [FromBody] Fornecedor fornecedor
            )
        {
            fornecedor.setDate();
            try
            {
                await _fornecedorContext.Fornecedores.InsertOneAsync(fornecedor);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Fornecedor = fornecedor });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao criar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }
        }
        [HttpPut]
        public async Task<ActionResult<Fornecedor>> put(
            [FromBody] Fornecedor fornecedor
            )
        {
            try
            {
                 await _fornecedorContext.Fornecedores.ReplaceOneAsync<Fornecedor>(x => x.Id == fornecedor.Id, fornecedor);
                return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Falha ao atualizar", complemento = "um ou mais erros ocorreram", status = "400", error = e.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fornecedor>> Delete(string id)
        {
            try
            {
                var response = await _fornecedorContext.Fornecedores.FindOneAndDeleteAsync<Fornecedor>(x => x.Id == id);

                return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, DeletedProdut = response });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Um ou mais erros aconteceram", status = "400", erro = e.Message, trace = e.StackTrace });

            }


        }
    }
}
