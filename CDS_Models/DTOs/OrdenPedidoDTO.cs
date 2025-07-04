using System;

namespace CDS_Models.DTOs
{
    public class OrdenPedidoDTO
    {
        public int IdOpci { get; set; }
        public int? IdFp { get; set; }
        public int? IdCliente { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdOpd { get; set; }
        public DateTime? FecRecepcion { get; set; }
        public DateTime? FecInicio { get; set; }
        public DateTime? FecProcVi { get; set; }
        public string? RazonSocialCliente { get; set; }
        public string? NumOp { get; set; }
        public int? IdMda { get; set; }
        public decimal? TotalSinIgv { get; set; }
        public string? NumRefCliente { get; set; }
        public string? ClienteFinal { get; set; }
        public string? ClienteProveedor { get; set; }
        public string? Vendedor1 { get; set; }
        public string? Vendedor2 { get; set; }
        public string? Lider { get; set; }
        public string? UbrutaCoti { get; set; }
        public bool? ComisionCompartida { get; set; }
        public bool? Estado { get; set; }
    }
}
