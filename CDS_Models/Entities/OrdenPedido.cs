using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models.Entities
{
    [Table("OrdPedido")]
    public class OrdenPedido
    {
        [Key, Column("Id_OPCI")]
        public string? IdOpci { get; set; }

        public DateTime? FecRecep { get; set; }
        public DateTime? FecInicio { get; set; }

        [Column("FecProcVI")]
        public DateTime? FecProcVi { get; set; }

        [Column("NumOP", TypeName = "nvarchar(100)")]
        public string? NumOp { get; set; }

        [Column("Id_Mda")]
        public int? IdMda { get; set; }

        [Column("Id_FP")]
        public int? IdFp { get; set; }

        [Column("Id_Clt", TypeName = "char(10)")]
        public string? IdClt { get; set; }

        [Column("RSocialClt", TypeName = "nvarchar(100)")]
        public string? RsocialClt { get; set; }

        [Column("Id_Vdr")]
        public string IdVdr { get; set; } = null!;

        [Column("NomVdr")]
        public string? NomVdr { get; set; }

        [Column("TotalSinIGV", TypeName = "numeric(9, 2)")]
        public decimal? TotalSinIgv { get; set; }

        [Column("NumRefCliente", TypeName = "nvarchar(100)")]
        public string? NumRefCliente { get; set; }

        [Column("Ib_CltFinal", TypeName = "nvarchar(100)")]
        public string? IbCltFin { get; set; }

        [Column("Ib_CltPrv", TypeName = "nvarchar(100)")]
        public string? IbCltPrv { get; set; }

        [Column("Ib_Vdr1")]
        public int? IbVdr1 { get; set; }

        [Column("Ib_Vdr2")]
        public int? IbVdr2 { get; set; }

        [Column("Ib_Lider")]
        public int? IbLider { get; set; }

        [StringLength(10)]
        public string? UbrutaCoti { get; set; }

        [StringLength(50)]
        public string? ComisionCompartida { get; set; }

        public bool? Estado { get; set; }

        [ForeignKey("IdFp")]
        public virtual FormaPago? FormaPago { get; set; }

        [ForeignKey("IdClt")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("IdVdr")]
        public virtual Vendedor? Vendedor { get; set; }

        [ForeignKey("IdMda")]
        public virtual Moneda? Moneda { get; set; }

        public virtual ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalles { get; set; }
    }
}
