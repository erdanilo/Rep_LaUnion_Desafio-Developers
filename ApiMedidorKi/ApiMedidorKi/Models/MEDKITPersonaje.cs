//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiMedidorKi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEDKITPersonaje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEDKITPersonaje()
        {
            this.MEDKITLuchadorPersonaje = new HashSet<MEDKITLuchadorPersonaje>();
        }
    
        public int IdPersonaje { get; set; }
        public string NombrePersonaje { get; set; }
        public int PoderInicio { get; set; }
        public int PoderFin { get; set; }
        public string UsuarioInserto { get; set; }
        public System.DateTime FechaInserto { get; set; }
        public string UsuarioModifico { get; set; }
        public Nullable<System.DateTime> FechaModifico { get; set; }
        public bool Eliminado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDKITLuchadorPersonaje> MEDKITLuchadorPersonaje { get; set; }
    }
}
