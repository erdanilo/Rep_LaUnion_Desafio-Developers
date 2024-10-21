namespace ApiMedidorKi.Models.BE
{
    public class ResumenLuchadorBE
    {
        public int IdLuchadorPersonaje { get; set; }
        public string NombreLuchador { get; set; }
        public string UrlFotoLuchador { get; set; }
        public string NombrePersonaje { get; set; }
        public string UrlImagenPersonaje { get; set; }
        public int? Esferas { get; set; }
        public decimal? Ki { get; set; }
        
    }
}