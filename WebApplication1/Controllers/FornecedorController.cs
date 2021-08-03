using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.fornecedor;
using MongoDB.Driver;
using System.Net;
using WebApplication1.DomainInterfaces;

namespace WebApplication1.Controllers
{
    [Route("v1/fornecedores")]

    public class FornecedorController : Controller
    {

        private readonly IFornecedorRepository _repository;

        public FornecedorController(IFornecedorRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Fornecedor>>> index()
        {

            var response = await _repository.getAllAsync();

            return response;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> find(string id)
        {
            var fornecedor = await _repository.getByIdAsync(id);
            if (fornecedor != null)
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
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }

            try
            {
                var response = await _repository.storeAsync(fornecedor);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Fornecedor = fornecedor, Response = response });
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
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }

            var response = await _repository.updateAsync(fornecedor);
            return Json(new
            {
                message = "Atualizado com sucesso",
                status = "200",
                Data = DateTime.Now,
                New = fornecedor,
                Old = response
            });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fornecedor>> Delete(string id)
        {

            var response = await _repository.deleteAsync(id);

            return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, Response = response });

        }
    }
}
