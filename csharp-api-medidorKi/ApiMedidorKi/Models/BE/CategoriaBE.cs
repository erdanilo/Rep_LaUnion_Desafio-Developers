using System.Collections.Generic;

namespace ApiMedidorKi.Models.BE
{
    public class CategoriaBE
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public List<RetoBE> Retos { get; set; }
    }
}