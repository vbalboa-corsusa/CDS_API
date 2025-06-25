using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Proyecto
    {
        [Key]
        public int IdProy { get; set; }

        [StringLength(50)]
        public string? CodComercial { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public int? IdCc { get; set; }
        [Column("ID_SCC")]
        public int? IdScc { get; set; }
        [Column("ID_SSCC")]
        public int? IdSscc { get; set; }
        public bool? Estado { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }

        [ForeignKey("IdCc")]
        public CCosto? CCosto { get; set; }
        [ForeignKey("IdScc")]
        public ScCosto? ScCosto { get; set; }
        [ForeignKey("IdSscc")]
        public SscCosto? SscCosto { get; set; }
    }
}
