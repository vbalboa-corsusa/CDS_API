using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("CCosto")]
    public class CCosto
    {
        [Key, Column("Id_CC", Order = 0)]
        public string? IdCc { get; set; }

        [StringLength(50)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }

        public bool? Estado { get; set; }

        public ICollection<Proyecto>? ProyectosCCosto { get; set; }
        public ICollection<Servicio>? ServiciosCCosto { get; set; }
        public ICollection<Producto>? ProductosCCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesCCosto { get; set; }
    }
}
