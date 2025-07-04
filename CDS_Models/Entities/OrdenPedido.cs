using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models.Entities
{
public class OrdenPedido
    {
        [Key]
        public int IdOpci { get; set; }

        public int? IdFp { get; set; }
        public int? IdCliente { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdOpd { get; set; }
        public DateTime? FecRecepcion { get; set; }
        public DateTime? FecInicio { get; set; }
        public DateTime? FecProcVi { get; set; }

        [StringLength(100)]
        public string? RazonSocialCliente { get; set; }

        [StringLength(200)]
        public string? NumOp { get; set; }
        public int? IdMda { get; set; }
        public decimal? TotalSinIgv { get; set; }

        [StringLength(50)]
        public string? NumRefCliente { get; set; }

        [StringLength(100)]
        public string? ClienteFinal { get; set; }

        [StringLength(100)]
        public string? ClienteProveedor { get; set; }

        [StringLength(50)]
        public string? Vendedor1 { get; set; }

        [StringLength(50)]
        public string? Vendedor2 { get; set; }

        [StringLength(50)]
        public string? Lider { get; set; }

        [StringLength(10)]
        public string? UbrutaCoti { get; set; }

        public bool? ComisionCompartida { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdFp")]
        public FormaPago? FormaPago { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("IdVendedor")]
        public Vendedor? Vendedor { get; set; }

        [ForeignKey("IdMda")]
        public Moneda? Moneda { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalles { get; set; }
    }
}
