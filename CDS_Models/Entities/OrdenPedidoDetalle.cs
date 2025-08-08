using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models.Entities
{
    [Table("OrdPedidoDet")]
    public class OrdenPedidoDetalle
    {
        [Key, Column("Id_OPCI")]
        public string? IdOpci { get; set; }

        [Column("Id_Prd", TypeName = "char(10)")]
        public string? IdPrd { get; set; }

        [Column("Id_Srv", TypeName = "char(10)")]
        public string? IdSrv { get; set; }

        [Column("Id_Pry", TypeName = "char(10)")]
        public string? IdPry { get; set; }

        [StringLength(5)]
        public string? ItemOp { get; set; }

        [StringLength(50)]
        public string? CodCom1 { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Cant { get; set; }

        [Column("Id_UM")]
        public int? IdUm { get; set; }

        [Column("Id_Mda")]
        public int? IdMda { get; set; }

        [Column("PrecioVentaUnit", TypeName = "decimal(10, 2)")]
        public decimal? PreVentUn { get; set; }

        public DateTime? FecReqCli { get; set; }

        [Column("PT_Estimado", TypeName = "decimal(10, 2)")]
        public decimal? PtEstimado { get; set; }

        [Column("Id_TC")]
        public int? IdTc { get; set; }

        [Column("Id_TN")]
        public int? IdTn { get; set; }

        [Column("Id_STN")]
        public int? IdStn { get; set; }

        [Column("Id_SSTN")]
        public int? IdSstn { get; set; }

        [Column("TE_Sem")]
        public int? TeSem { get; set; }

        [StringLength(20)]
        public string? NumCoti { get; set; }

        [Column("Ib_Armado")]
        public bool? IbArmado { get; set; }

        [Column("Cod_Cliente", TypeName = "nvarchar(20)")]
        public string? CodCliente { get; set; }

        [StringLength(50)]
        public string? NumDeal { get; set; }

        [StringLength(50)]
        public string? NumSrv { get; set; }

        [StringLength(50)]
        public string? NumPry { get; set; }

        // Revisar si es correcto o no
        public int? Id { get; set; }

        [Column("Id_CC")]
        public string? IdCc { get; set; }

        [Column("Id_SCC")]
        public string? IdScC { get; set; }

        [Column("Id_SSCC")]
        public string? IdSscC { get; set; }

        [StringLength(400)]
        public string? Nota1 { get; set; }

        [StringLength(400)]
        public string? Nota2 { get; set; }

        [StringLength(400)]
        public string? Nota3 { get; set; }

        [StringLength(400)]
        public string? Nota4 { get; set; }

        [ForeignKey("IdPrd")]
        public Producto? Producto { get; set; }
        [ForeignKey("IdSrv")]
        public Servicio? Servicio { get; set; }
        [ForeignKey("IdPry")]
        public Proyecto? Proyecto { get; set; }
        [ForeignKey("IdUm")]
        public UnidadMedida? UnidadMedida { get; set; }
        public Moneda? Moneda { get; set; }
        public TcUsd? TcUsd { get; set; }

        [ForeignKey("IdCc")]
        public virtual CCosto? CCosto { get; set; }
        [ForeignKey("IdScC")]
        public virtual ScCosto? ScCosto { get; set; }

        [ForeignKey("IdSscC")]
        public virtual SscCosto? SscCosto { get; set; }

        [ForeignKey("IdOpci")]
        public CDS_Models.Entities.OrdenPedido? OrdenPedido { get; set; }

        public SubSubTipoNegocio? SubSubTiposNegocio { get; set; }

        public SubTipoNegocio? SubTiposNegocio { get; set; }

        [ForeignKey("IdTn")]
        public TipoNegocio? TipoNegocio { get; set; }

        [Column("Id_EstOP")]
        public int? IdEstOp { get; set; }
        [ForeignKey("IdEstOp")]
        public virtual EstadosOp? EstadosOp { get; set; }
    }
}
