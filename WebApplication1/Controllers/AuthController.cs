using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using WebApplication1.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using BC = BCrypt.Net.BCrypt;

namespace WebApplication1.Controllers
{
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        
        private readonly UserContext _userContext;

       
        public AuthController(IOptions<ConfigDb> opcoes)
        {
            _userContext = new UserContext(opcoes);
        }

        [HttpGet]
        [Route("index")]
       // [Authorize(Roles ="admin")]
        public async Task<ActionResult<List<User>>> index()
        {
            var users = await _userContext.Users.Find(x => true).ToListAsync();
            return users;
        }
        [HttpPost]
        public async Task<ActionResult<dynamic>> store(
            [FromBody] User user)
        {
            var verify = await _userContext.Users.Find(x => x.user == user.user).FirstOrDefaultAsync();

            if (verify != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                
                return Json(new { message = "Nome de usuario ja existente", status = "400", usuario = user.user});
            }
            else
            {
                string hashPassword = BC.HashPassword(user.password);
                user.password = hashPassword;
                await _userContext.Users.InsertOneAsync(user);
                return Json(new { message = "Criado com sucesso", status = "200", Data = DateTime.Now });
            }
            
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> login([FromBody] User model)
        {
            
            var user = await _userContext.Users.Find(x=>x.user == model.user).FirstOrDefaultAsync();

            if(user == null || !BC.Verify(model.password, user.password))
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Usuario não encontrado", complemento = "verifique o criterio de busca", status = "400" });
            }
            user.password = "";
            var token = TokenServices.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };

        }
    }
}
