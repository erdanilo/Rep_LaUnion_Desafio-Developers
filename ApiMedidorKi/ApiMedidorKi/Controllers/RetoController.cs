using ApiMedidorKi.Models;
using System.Linq;
using System.Web.Http;


namespace ApiMedidorKi.Controllers
{
    [Authorize]
    public class RetoController : ApiController
    {
        dbDesafioEntities contexto = new dbDesafioEntities();
        
        [HttpGet]
        [Route("api/reto")]
        public IHttpActionResult Get()
        {
            contexto.Configuration.LazyLoadingEnabled = false;
            contexto.Configuration.ProxyCreationEnabled = false;

            var listaRetos = (from reto in contexto.MEDKITReto
                              join categoria in contexto.MEDKITCategoria on reto.IdCategoria equals categoria.IdCategoria
                              where !reto.Eliminado && !categoria.Eliminado
                              select new {
                                  categoria.IdCategoria,
                                  categoria.NombreCategoria,
                                  reto.IdReto,
                                  reto.NombreReto
                              }).ToList();
            return Ok(listaRetos);
        }
        
    }
}