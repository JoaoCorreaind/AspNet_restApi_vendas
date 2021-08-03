using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using WebApplication1.DomainInterfaces;

namespace WebApplication1.Controllers
{
    [Route("v1/vendedores")]

    public class VendedorController : Controller
    {
        private readonly VendedorContext _vendedorContext;
        private readonly IVendedorRepository _repository;

        public VendedorController(IOptions<ConfigDb> opcoes, IVendedorRepository repository)
        {
            _vendedorContext = new VendedorContext(opcoes);
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vendedor>>> index()
        {
            var vendedores = await _repository.getAllAsync();
            return vendedores;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> find(string id)
        {
            var vendedor = await _repository.getByIdAsync(id);
            if (vendedor != null)
            {
                return vendedor;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vendedor>> store(

            [FromBody] Vendedor vendedor
            )
        {
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            await _repository.storeAsync(vendedor);
            return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now, Vendedor = vendedor });
        }

        [HttpPut]
        public async Task<ActionResult<Vendedor>> update(
           [FromBody] Vendedor vendedor
           )
        {
            if (!ModelState.IsValid) //<----Validate here
            {
                return new BadRequestObjectResult(ModelState);
            }
            var response = await _repository.updateAsync(vendedor);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendedor>> Delete(string id)
        {
            var response = await _repository.deleteAsync(id);
            return response;
        }
    }
}
