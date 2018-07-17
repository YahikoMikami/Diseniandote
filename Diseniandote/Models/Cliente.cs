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
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.DatosEnvio = new HashSet<DatosEnvio>();
            this.DatosFacturacion = new HashSet<DatosFacturacion>();
        }
    
        public int idCliente { get; set; }
        public int idTarjeta { get; set; }
        public int idPersona { get; set; }
        public int idUsuario { get; set; }
        public bool estatus { get; set; }
    
        public virtual Persona Persona { get; set; }
        public virtual Tarjeta Tarjeta { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DatosEnvio> DatosEnvio { get; set; }
        public virtual ICollection<DatosFacturacion> DatosFacturacion { get; set; }
    }
}