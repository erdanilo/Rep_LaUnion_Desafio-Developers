using ApiMedidorKi.Models;
using System.Linq;
using System.Web.Http;


namespace ApiMedidorKi.Controllers
{
    [Authorize]
    public class LuchadorController : ApiController
    {
        dbDesafioEntities contexto = new dbDesafioEntities();
        
        [HttpGet]
        [Route("api/luchador")]
        public IHttpActionResult Get()
        {
            var listaLuchadores = contexto.MEDKITLuchador.Where(x=> !x.Eliminado).ToList();

            return Ok(listaLuchadores);
        }

        [HttpGet]
        [Route("api/luchador/detalle")]
        public IHttpActionResult Detalle(int idLuchadorPersonaje)
        {
            var detalleLuchador = (from lp in contexto.MEDKITLuchadorPersonaje
                                   join p in contexto.MEDKITPersonaje on lp.IdPersonaje equals p.IdPersonaje
                                   join l in contexto.MEDKITLuchador on lp.IdLuchador equals l.IdLuchador
                                   where lp.IdLuchadorPersonaje == idLuchadorPersonaje
                                   select new {
                                       l.NombreLuchador,
                                       l.CorreoLuchador,
                                       p.NombrePersonaje,
                                       p.PoderInicio,
                                       p.PoderFin,
                                       lp.Ki,
                                       lp.Esferas,
                                       listaHabilidadesObtenidas = (from c in contexto.MEDKITCalificacion
                                                                    join r in contexto.MEDKITReto on c.IdReto equals r.IdReto
                                                                    join cat in contexto.MEDKITCategoria on r.IdCategoria equals cat.IdCategoria
                                                                    where !c.Eliminado && c.Puntaje > 70 && c.IdLuchadorPersonaje == lp.IdLuchadorPersonaje
                                                                    group c by new { cat.NombreCategoria, r.NombreReto } into agrupacion
                                                                    select new {
                                                                        agrupacion.Key.NombreCategoria,
                                                                        agrupacion.Key.NombreReto
                                                                    }).ToList(),
                                        listaRestosPendientes = (from c in contexto.MEDKITCalificacion
                                                                 join r in contexto.MEDKITReto on c.IdReto equals r.IdReto
                                                                 join cat in contexto.MEDKITCategoria on r.IdCategoria equals cat.IdCategoria
                                                                 where !c.Eliminado && c.Puntaje <= 70 && c.IdLuchadorPersonaje == lp.IdLuchadorPersonaje
                                                                 group c by new { cat.NombreCategoria, r.NombreReto } into agrupacion
                                                                 select new
                                                                 {
                                                                     agrupacion.Key.NombreCategoria,
                                                                     agrupacion.Key.NombreReto
                                                                 }).ToList()
                                   }).ToList();

            return Ok(detalleLuchador);
        }

    }
}