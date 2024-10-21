using ApiMedidorKi.Clases;
using System.Web.Http;


namespace ApiMedidorKi.Controllers
{
    [Authorize]
    public class LuchadorController : ApiController
    {   
        [HttpGet]
        [Route("api/luchador")]
        public IHttpActionResult Get()
        {
            Luchador luchadores = new Luchador();

            return Ok( luchadores.GetLuchadores() );
        }

        [HttpGet]
        [Route("api/luchador/detalle/{idLuchadorPersonaje}")]
        public IHttpActionResult Detalle(int idLuchadorPersonaje)
        {
            Luchador luchador = new Luchador();

            return Ok( luchador.GetDetalle(idLuchadorPersonaje) );
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/luchador/resumen")]
        public IHttpActionResult Resumen()
        {
            Luchador luchadores = new Luchador();

            return Ok( luchadores.GetResumen() );
        }

    }
}