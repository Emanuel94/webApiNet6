using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0)
                {
                    return new
                    {
                        succes = false,
                        message = "Verificar si estas enviando un token invalido",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                User user = new User();

                //user = user.DB().FirstOrDefault(x => x.Id == id);

                return new
                {
                    succes = true,
                    message = "Verificar si estas enviando un token invalido",
                    result = user
                };

            }
            catch (Exception ex)
            {

                return new
                {
                    succes = true,
                    message = "Verificar si estas enviando un token invalido",
                    result = ""
                };
            }
        }
    }
}
