using ApiMedidorKi.Models;
using ApiMedidorKi.Models.BE;
using System.Collections.Generic;
using System.Linq;


namespace ApiMedidorKi.Clases
{
    public class Luchador
    {
        dbDesafioEntities db = new dbDesafioEntities();

        internal List<LuchadorBE> GetLuchadores()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            var listaLuchadores = db.MEDKITLuchador.Where(x => !x.Eliminado).Select(y=>
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
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            var listaResumen = (from lp in db.MEDKITLuchadorPersonaje
                                join l in db.MEDKITLuchador on lp.IdLuchador equals l.IdLuchador
                                join p in db.MEDKITPersonaje on lp.IdPersonaje equals p.IdPersonaje
                                where !lp.Eliminado
                                select new ResumenLuchadorBE
                                {
                                    NombreLuchador = l.NombreLuchador,
                                    UrlFotoLuchador = l.UrlFoto,
                                    NombrePersonaje = p.NombrePersonaje,
                                    UrlImagenPersonaje = p.UrlPersonaje,
                                    Esferas = lp.Esferas,
                                    Ki = lp.Ki
                                }).ToList();

            return listaResumen;
        }

    }
}