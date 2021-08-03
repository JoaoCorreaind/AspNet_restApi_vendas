using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models.venda;

using WebApplication1.DomainInterfaces;

namespace WebApplication1.Controllers
{
    [Route("v1/vendas")]
    public class VendasController : Controller
    {
        private readonly IVendaRepository _repository;


        public VendasController(IVendaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Venda>>> index()
        {

            var vendas = await _repository.getAllAsync();

            return vendas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> find(string id)
        {
            var venda = await _repository.getByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return venda;
        }

        [HttpPost]
        public async Task<ActionResult<Venda>> store(

            [FromBody] Venda venda
            )
        {
            await _repository.storeAsync(venda);
            return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Venda = venda });

        }

        [HttpPut]
        public async Task<ActionResult<Venda>> update(
           [FromBody] Venda venda
           )
        {

            var response = await _repository.updateAsync(venda);
            return Json(new { message = "Atualizado com sucesso", status = "200", Data = DateTime.Now, Response = response });

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Venda>> Delete(string id)
        {

            var response = await _repository.deleteAsync(id);

            return Json(new { message = "deletado com sucesso", status = "200", deleted_at = DateTime.Now, DeletedCategoria = response });


        }
    }
}
