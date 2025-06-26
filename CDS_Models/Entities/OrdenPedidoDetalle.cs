using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models.Entities
{
    public class OrdenPedidoDetalle
    {
        [Key]
        public int IdOpd { get; set; }
        public int? IdStatus { get; set; }
        public int? IdTn { get; set; }
        public int? IdStn { get; set; }
        public int? IdSstn { get; set; }
        public int? IdProd { get; set; }
        public int? IdServ { get; set; }
        public int? IdProy { get; set; }

        [StringLength(5)]
        public string? ItemOp { get; set; }

        [StringLength(50)]
        public string? CodComercial { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Cantidad { get; set; }
        public int? IdUm { get; set; }
        public int? IdMda { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Pvu { get; set; }
        public DateTime? FecReqCli { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PtEstimado { get; set; }
        public int? IdTc { get; set; }
        public int? TeSem { get; set; }

        [StringLength(20)]
        public string? NumCoti { get; set; }
        public bool? IbArmado { get; set; }

        [StringLength(20)]
        public string? CodCliente { get; set; }

        [StringLength(50)]
        public string? NumDeal { get; set; }

        [StringLength(50)]
        public string? NumServicio { get; set; }

        [StringLength(50)]
        public string? NumProyecto { get; set; }
        public string? IdCc { get; set; }
        public string? IdScc { get; set; }
        public string? IdSscc { get; set; }

        [StringLength(400)]
        public string? Nota1 { get; set; }

        [StringLength(400)]
        public string? Nota2 { get; set; }

        [StringLength(400)]
        public string? Nota3 { get; set; }

        [StringLength(400)]
        public string? Nota4 { get; set; }
        public bool? Estado { get; set; }
        public Producto? Producto { get; set; }
        public Servicio? Servicio { get; set; }
        public Proyecto? Proyecto { get; set; }
        public UnidadMedida? UnidadMedida { get; set; }
        public Moneda? Moneda { get; set; }
        public TcUsd? TcUsd { get; set; }
        public CCosto? CCosto { get; set; }
        public ScCosto? ScCosto { get; set; }
        public SscCosto? SscCosto { get; set; }
        public int? IdOpci { get; set; }
        [ForeignKey("IdOpci")]
        public CDS_Models.Entities.OrdenPedido? OrdenPedido { get; set; }
        [ForeignKey("IdSstn")]
        public SubSubTiposNegocio? SubSubTiposNegocio { get; set; }
        [ForeignKey("IdStn")]
        public SubTiposNegocio? SubTiposNegocio { get; set; }
        [ForeignKey("IdTn")]
        public TiposNegocio? TiposNegocio { get; set; }
        [ForeignKey("IdStatus")]
        public StatusOp? StatusOp { get; set; }
    }
}
