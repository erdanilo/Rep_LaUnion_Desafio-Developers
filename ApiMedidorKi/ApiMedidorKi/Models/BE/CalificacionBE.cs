using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMedidorKi.Models.BE
{
    public class CalificacionBE
    {
        public int IdCalificacion { get; set; }
        public int IdLuchadorPersonaje { get; set; }
        public int IdReto { get; set; }
        public int Puntaje { get; set; }
        public string UsuarioInserto { get; set; }
    }
}