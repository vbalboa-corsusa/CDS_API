using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Servicio
    {
        [Key]
        public int IdServ { get; set; }

        [StringLength(50)]
        public string? CodComercial { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public string? IdCc { get; set; }
        public string? IdScc { get; set; }
        public string? IdSscc { get; set; }
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
