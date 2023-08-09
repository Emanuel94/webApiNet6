using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Asp.Application.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthV1Controller : Controller
    {
        private IConfiguration _configuration;
        public AuthV1Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] object Data)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());

            string userName = data.user.ToString();
            string password = data.Password.ToString();
        
            User user = new User();
           // user = user.DB().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return StatusCode(403, "Usuario o contrasena invalida");
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Usuario", user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: singIn
               );

            return new
            {
                success = true,
                message = "Exito",
                results = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var token = Jwt.validarToken(identity);

            if (!token.succes) return token;

            User usuario = token.result;

            return new
            {
                success = true,
                message = "cliente Eliminado",
                result = ""
            };

        }


    }
}
