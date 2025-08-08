using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("Proyecto")]
    public class Proyecto
    {
        [Key, Column("Id_Pry", TypeName = "char(10)")]
        public string? IdPry { get; set; }

        [StringLength(50)]
        public string? CodCom1 { get; set; }

        [StringLength(200)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }

        public string? IdCc { get; set; }
        [Column("ID_SCC")]
        public string? IdScC { get; set; }
        [Column("ID_SSCC")]
        public string? IdSscC { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdCc")]
        public CCosto? CCosto { get; set; }
        
        public ScCosto? ScCosto { get; set; }

        public SscCosto? SscCosto { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
