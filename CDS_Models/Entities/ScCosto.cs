using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("SCCosto")]
    public class ScCosto
    {
        [Key, Column("Id_SCC", Order = 1)]
        [StringLength(10)]
        public string? IdScC { get; set; }

        [Column("Id_CC", Order = 0)]
        [StringLength(10)]
        public string? IdCc { get; set; }

        [ForeignKey(nameof(IdCc))]
        public CCosto? CCosto { get; set; }

        public ICollection<SscCosto>? SscCosto { get; set; }
        public ICollection<Producto>? ProductosScCosto { get; set; }
        public ICollection<Servicio>? ServiciosScCosto { get; set; }
        public ICollection<Proyecto>? ProyectosScCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesScCosto { get; set; }
    }
}
