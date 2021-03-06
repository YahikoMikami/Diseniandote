//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diseniandote.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Domicilio
    {
        public Domicilio()
        {
            this.DatosEnvio = new HashSet<DatosEnvio>();
            this.Persona = new HashSet<Persona>();
        }
    
        public int idDomicilio { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string cp { get; set; }
        public string numeroInt { get; set; }
        public string numeroExt { get; set; }
        public Nullable<bool> estatus { get; set; }
        public int idCiudad { get; set; }
    
        public virtual Ciudad Ciudad { get; set; }
        public virtual ICollection<DatosEnvio> DatosEnvio { get; set; }
        public virtual DatosFacturacion DatosFacturacion { get; set; }
        public virtual ICollection<Persona> Persona { get; set; }
    }
}
