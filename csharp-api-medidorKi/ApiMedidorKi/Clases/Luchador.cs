using ApiMedidorKi.Models;
using ApiMedidorKi.Models.BE;
using System.Collections.Generic;
using System.Linq;


namespace ApiMedidorKi.Clases
{
    public class Luchador
    {
        dbDesafioEntities context = new dbDesafioEntities();

        internal List<LuchadorBE> GetLuchadores()
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            var listaLuchadores = context.MEDKITLuchador.Where(x => !x.Eliminado).Select(y=>
                new LuchadorBE
                {
                    Id = y.IdLuchador,
                    Nombre = y.NombreLuchador,
                    Correo = y.CorreoLuchador,
                    UrlFoto = y.UrlFoto
                }).ToList();

            return listaLuchadores;
        }

        internal List<ResumenLuchadorBE> GetResumen()
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            var listaResumen = (from lp in context.MEDKITLuchadorPersonaje
                                join l in context.MEDKITLuchador on lp.IdLuchador equals l.IdLuchador
                                join p in context.MEDKITPersonaje on lp.IdPersonaje equals p.IdPersonaje
                                where !lp.Eliminado
                                select new ResumenLuchadorBE
                                {
                                    IdLuchadorPersonaje = lp.IdLuchadorPersonaje,
                                    NombreLuchador = l.NombreLuchador,
                                    UrlFotoLuchador = l.UrlFoto,
                                    NombrePersonaje = p.NombrePersonaje,
                                    UrlImagenPersonaje = p.UrlPersonaje,
                                    Esferas = lp.Esferas,
                                    Ki = lp.Ki
                                }).ToList();

            return listaResumen;
        }

        internal dynamic GetDetalle(int idLuchadorPersonaje)
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            var detalleLuchador = (from lp in context.MEDKITLuchadorPersonaje
                                   join p in context.MEDKITPersonaje on lp.IdPersonaje equals p.IdPersonaje
                                   join l in context.MEDKITLuchador on lp.IdLuchador equals l.IdLuchador
                                   where lp.IdLuchadorPersonaje == idLuchadorPersonaje
                                   select new DetalleLuchadorBE
                                   {
                                       NombreLuchador = l.NombreLuchador,
                                       UrlFotoLuchador = l.UrlFoto,
                                       NombrePersonaje = p.NombrePersonaje,
                                       UrlImagenPersonaje = p.UrlPersonaje,
                                       Esferas = lp.Esferas,
                                       Ki = lp.Ki,
                                       HabilidadesObtenidas = (from c in context.MEDKITCalificacion
                                                                join r in context.MEDKITReto on c.IdReto equals r.IdReto
                                                                join cat in context.MEDKITCategoria on r.IdCategoria equals cat.IdCategoria
                                                                where !c.Eliminado && c.Activo && c.Puntaje >= r.PunteoAprobacion && c.IdLuchadorPersonaje == lp.IdLuchadorPersonaje
                                                                group c by new { cat.IdCategoria, cat.NombreCategoria, r.IdReto, r.NombreReto, c.Puntaje } into agrupacion
                                                                select new CategoriaBE
                                                                {
                                                                    IdCategoria = agrupacion.Key.IdCategoria,
                                                                    NombreCategoria = agrupacion.Key.NombreCategoria,
                                                                    Retos = (from re in context.MEDKITReto
                                                                             where !re.Eliminado && re.IdReto == agrupacion.Key.IdReto
                                                                             select new RetoBE
                                                                             {
                                                                                 IdReto = re.IdReto,
                                                                                 NombreReto = re.NombreReto,
                                                                                 PunteoAprobacion = re.PunteoAprobacion,
                                                                             }).ToList()
                                                                }).ToList(),
                                       Categorias = (from c in context.MEDKITCategoria                                                             
                                                    where !c.Eliminado
                                                    select new CategoriaBE
                                                    {
                                                        IdCategoria = c.IdCategoria,
                                                        NombreCategoria = c.NombreCategoria,
                                                        Retos = (from r in context.MEDKITReto
                                                                 where !r.Eliminado && c.IdCategoria == r.IdCategoria
                                                                 select new RetoBE {
                                                                     IdReto = r.IdReto,
                                                                     NombreReto = r.NombreReto,
                                                                     PunteoAprobacion = r.PunteoAprobacion
                                                                 }).ToList()
                                                    }).ToList()
                                   }).ToList();

            return detalleLuchador;
        }

    }
}