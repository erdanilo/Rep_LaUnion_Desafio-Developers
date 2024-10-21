using System.Collections.Generic;

namespace ApiMedidorKi.Models.BE
{
    public class DetalleLuchadorBE
    {
        public string NombreLuchador { get; set; }
        public string UrlFotoLuchador { get; set; }
        public string NombrePersonaje { get; set; }
        public string UrlImagenPersonaje { get; set; }
        public int? Esferas { get; set; }
        public decimal? Ki { get; set; }
        public List<CategoriaBE> HabilidadesObtenidas { get; set; }
        public List<CategoriaBE> Categorias { get; set; }
    }
}