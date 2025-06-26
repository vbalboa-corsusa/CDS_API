using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class CCosto
    {
        [Key]
        [StringLength(16)]
        public string IdCc { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NumCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Proyecto>? ProyectosCCosto { get; set; }
        public ICollection<Servicio>? ServiciosCCosto { get; set; }
        public ICollection<Producto>? ProductosCCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesCCosto { get; set; }
    }
}
