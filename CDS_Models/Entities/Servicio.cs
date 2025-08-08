using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("Servicio")]
    public class Servicio
    {
        [Key, Column("Id_Srv", TypeName = "char(10)")]
        public string? IdSrv { get; set; }

        [StringLength(50)]
        public string? CodCom1 { get; set; }

        [StringLength(200)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }
        public string? IdCc { get; set; }
        public string? IdScC { get; set; }
        public string? IdSscC { get; set; }
        public bool? Estado { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }

        [ForeignKey("IdCc")]
        public CCosto? CCosto { get; set; }
        public ScCosto? ScCosto { get; set; }
        public SscCosto? SscCosto { get; set; }
    }
}
