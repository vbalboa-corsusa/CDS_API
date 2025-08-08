using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Producto
    {
        [Key, Column("Id_Prd", TypeName = "char(10)")]
        public string? IdPrd { get; set; }

        [Column("Id_Mca")]
        public int? IdMca { get; set; }

        [Column (TypeName = "nvarchar(200)")]
        public string? CodCom1 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? CodCom2 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? CodCom3 { get; set; }

        [StringLength(200)]
        public string? Descrip { get; set; }

        [StringLength(50)]
        public string? NCorto { get; set; }

        [Column("Id_Cls")]
        public int? IdCls { get; set; }
        [Column("Id_SCls")]
        public int? IdSCls { get; set; }
        
        [Column("Id_SSCls")]
        public int? IdSsCls { get; set; }
        
        [Column("Id_CC")]
        public string? IdCc { get; set; }
        
        [Column("Id_SCC")]
        public string? IdScC { get; set; }
        
        [Column("Id_SSCC")]
        public string? IdSscC { get; set; }
        public bool? Estado { get; set; }
        public ICollection<ProdUm>? ProdUm { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }

        [ForeignKey("IdMca")]
        public Marca? Marca { get; set; }

        [ForeignKey("IdCls")]
        public Clase? Clase { get; set; }

        [ForeignKey("IdSCls")]
        public SubClase? SubClase { get; set; }
        
        [ForeignKey("IdSsCls")]
        public SubSubClase? SubSubClase { get; set; }
        
        [ForeignKey("IdCc")]
        public CCosto? CCosto { get; set; }
        
        public ScCosto? ScCosto { get; set; }
        
        public SscCosto? SscCosto { get; set; }
    }
}
