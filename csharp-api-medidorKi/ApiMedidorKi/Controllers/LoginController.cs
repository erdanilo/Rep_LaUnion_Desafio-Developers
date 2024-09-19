using System;
using System.Web.Http;
using System.Threading.Tasks;
using ApiMedidorKi.Models.LoginApi;
using ApiMedidorKi.Models;
using System.Linq;


namespace ApiMedidorKi.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Api/Login")]
        public async Task<IHttpActionResult> Autenticacion(Credenciales credenciales)
        {
            try
            {
                GeneraToken GeneraToken = new GeneraToken();
                Token TokenGenerado = new Token();

                if (ModelState.IsValid)//usuarioToken y passwordToken, usuario y contrasenia son requeridos en la clase Credenciales
                {
                    dbDesafioEntities contexto = new dbDesafioEntities();

                    // Validar usuario en la base de datos
                    var usuario = contexto.MEDKITUsuario.Where(x=> !x.Eliminado && x.Usuario == credenciales.Usuario && x.ClaveUsuario == credenciales.Contrasenia).FirstOrDefault();
                    if (usuario != null)
                    {
                        TokenGenerado = await GeneraToken.Generar(credenciales.UsuarioToken, credenciales.PasswordToken);
                        if (TokenGenerado.Estado)
                        {
                            return Ok(TokenGenerado);
                        }
                        else
                        {
                            return BadRequest(TokenGenerado.Error_Description); ;
                        }
                    }
                    else
                    {
                        return BadRequest("Usuario o clave incorrecto");
                    }
                }
                else
                {
                    return BadRequest("Datos incompletos.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}