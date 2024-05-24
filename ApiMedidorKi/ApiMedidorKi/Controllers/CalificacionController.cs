using ApiMedidorKi.Models;
using ApiMedidorKi.Models.BE;
using System;
using System.Linq;
using System.Web.Http;


namespace ApiMedidorKi.Controllers
{
    [Authorize]
    public class CalificacionController : ApiController
    {
        dbDesafioEntities contexto = new dbDesafioEntities();
        
        [HttpPost]
        [Route("api/calificacion/crear")]
        public IHttpActionResult Post([FromBody]CalificacionBE pCalificacion)
        {
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // aumento el ki
                    var _luchadorPersonajeActual = contexto.MEDKITLuchadorPersonaje.Where(x => x.IdLuchadorPersonaje == pCalificacion.IdLuchadorPersonaje).FirstOrDefault();
                    _luchadorPersonajeActual.Ki = _luchadorPersonajeActual.Ki + pCalificacion.Puntaje;
                    contexto.SaveChanges();

                    // agrego la calificacion obtenida en el reto
                    var idCalificacion = contexto.MEDKITCalificacion.Max(x => x.IdCalificacion);
                    if (idCalificacion == 0)
                    {
                        idCalificacion = 1;
                    }
                    else
                    {
                        idCalificacion = idCalificacion + 1;
                    }

                    MEDKITCalificacion _calificacion = new MEDKITCalificacion
                    {
                        IdCalificacion = idCalificacion,
                        IdLuchadorPersonaje = pCalificacion.IdLuchadorPersonaje,
                        IdReto = pCalificacion.IdReto,
                        Puntaje = pCalificacion.Puntaje,
                        UsuarioInserto = pCalificacion.UsuarioInserto,
                        FechaInserto = DateTime.Now,
                        Activo = true,
                        Eliminado = false
                    };

                    contexto.MEDKITCalificacion.Add(_calificacion);
                    contexto.SaveChanges();

                    // if punteo es mayor a 70
                    if (pCalificacion.Puntaje > 70)
                    {
                        // aumento en 1 las esferas obtenidas
                        _luchadorPersonajeActual.Esferas = _luchadorPersonajeActual.Esferas + 1;
                        contexto.SaveChanges();
                    }


                    //voy a ver que personaje tiene actualmente
                    var _personajeActual = (from lp in contexto.MEDKITLuchadorPersonaje
                                            join p in contexto.MEDKITPersonaje on lp.IdPersonaje equals p.IdPersonaje
                                            where !p.Eliminado && !lp.Eliminado && lp.IdLuchadorPersonaje == pCalificacion.IdLuchadorPersonaje
                                            select new
                                            {
                                                p.IdPersonaje,
                                                p.NombrePersonaje,
                                                p.PoderInicio,
                                                p.PoderFin
                                            }).FirstOrDefault();

                    // verifico si se le debe cambiar de personaje
                    if (_luchadorPersonajeActual.Ki > _personajeActual.PoderFin)
                    {
                        var _personajeNuevoPoderInicio = _personajeActual.PoderFin + 1;
                        var _personajeNuevo = contexto.MEDKITPersonaje.Where(x => !x.Eliminado && x.PoderInicio == _personajeNuevoPoderInicio).FirstOrDefault();

                        // inserto el nuevo personaje
                        var idLuchadorPersonaje = contexto.MEDKITLuchadorPersonaje.Max(x => x.IdLuchadorPersonaje);
                        if (idLuchadorPersonaje == 0)
                        {
                            idLuchadorPersonaje = 1;
                        }
                        else
                        {
                            idLuchadorPersonaje = idLuchadorPersonaje + 1;
                        }

                        MEDKITLuchadorPersonaje _luchadorPersonajeNuevo = new MEDKITLuchadorPersonaje
                        {
                            IdLuchadorPersonaje = idLuchadorPersonaje,
                            IdLuchador = _luchadorPersonajeActual.IdLuchador,
                            IdPersonaje = _personajeNuevo.IdPersonaje,
                            UsuarioInserto = pCalificacion.UsuarioInserto,
                            FechaInserto = DateTime.Now,
                            Activo = true,
                            Eliminado = false
                        };

                        contexto.MEDKITLuchadorPersonaje.Add(_luchadorPersonajeNuevo);
                        contexto.SaveChanges();

                        // desactivo el personaje actual
                        _luchadorPersonajeActual.Activo = false;
                        contexto.SaveChanges();
                    }

                    transaction.Commit();

                    return Ok("Calificacion guardada con exito");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return InternalServerError(ex);
                }
            }
        }
        
        [HttpPut]
        [Route("api/calificacion/eliminar")]
        public IHttpActionResult Delete(int idCalificacion)
        {
            var calificacion = contexto.MEDKITCalificacion.Where(x => x.IdCalificacion == idCalificacion).FirstOrDefault();
            calificacion.Activo = false;
            calificacion.Eliminado = true;

            contexto.SaveChanges();

            return Ok("Calificacion eliminada correctamente");
        }

    }
}