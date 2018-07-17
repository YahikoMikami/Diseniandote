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
    
    public partial class Produccion
    {
        public Produccion()
        {
            this.DetalleProduccion = new HashSet<DetalleProduccion>();
        }
    
        public int idProduccion { get; set; }
        public int idTipoProduccion { get; set; }
        public double costoDisenio { get; set; }
        public double costoManoxTiempo { get; set; }
        public double porcentajeGanancia { get; set; }
        public double subtotalMateriales { get; set; }
        public int idProducto { get; set; }
        public int cantidadProducto { get; set; }
    
        public virtual ICollection<DetalleProduccion> DetalleProduccion { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual TipoProduccion TipoProduccion { get; set; }
    }
}