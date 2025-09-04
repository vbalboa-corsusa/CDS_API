using System;

namespace CDS_Models.DTOs
{
    public class OrdenPedidoDTO
    {
        public int IdOpci { get; set; }

        public DateTime? FecRecep { get; set; }
        public DateTime? FecInicio { get; set; }
        public DateTime? FecProcVi { get; set; }

        public string? NumOp { get; set; }
        public int? IdMda { get; set; }

        public int? IdFp { get; set; }
        public int? IdClt { get; set; }

        public string? RsocialClt { get; set; }

        public int? IdVdr { get; set; }
        
        public int? IdOpd { get; set; }

        public string? NomVdr { get; set; }

        public decimal? TotalSinIgv { get; set; }

        public string? NumRefCliente { get; set; }

        public string? IbCltFin { get; set; }

        public string? IbCltPrv { get; set; }

        public string? IbVdr1 { get; set; }

        public string? IbVdr2 { get; set; }

        public string? IbLider { get; set; }

        public string? UbrutaCoti { get; set; }

        public bool? ComisionCompartida { get; set; }

        public bool? Estado { get; set; }
    }
}
