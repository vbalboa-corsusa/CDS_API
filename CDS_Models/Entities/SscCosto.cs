using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("SSCCosto")]
    public class SscCosto
    {
        [Key, Column("Id_SSCC", Order = 2)]
        [StringLength(10)]
        public string? IdSscC { get; set; }

        [StringLength(10)]
        [Column("Id_SCC", Order = 1)]
        public string? IdScC { get; set; }

        [StringLength(10)]
        [Column("Id_CC", Order = 0)]
        public string? IdCc { get; set; }

        [ForeignKey(nameof(IdCc) + "," + nameof(IdScC))]
        public ScCosto? ScCosto { get; set; }

        public ICollection<Proyecto>? ProyectosSscCosto { get; set; }
        public ICollection<Servicio>? ServiciosSscCosto { get; set; }
        public ICollection<Producto>? ProductosSscCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesSscCosto { get; set; }
    }
}
