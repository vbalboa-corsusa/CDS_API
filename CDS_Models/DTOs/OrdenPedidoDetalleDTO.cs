using System;

namespace CDS_Models.DTOs
{
    public class OrdenPedidoDetalleDTO
    {
        public int IdOpci { get; set; }
        //public int? IdStatus { get; set; }
        public int? IdTn { get; set; }
        public int? IdStn { get; set; }
        public int? IdSstn { get; set; }
        public int? IdPrd { get; set; }
        public int? IdSrv { get; set; }
        public int? IdPry { get; set; }
        public string? ItemOp { get; set; }
        public string? CodCom1 { get; set; }
        public decimal? Cant { get; set; }
        public int? IdUm { get; set; }
        public int? IdMda { get; set; }
        public decimal? PreVentUn { get; set; }
        public DateTime? FecReqCli { get; set; }
        public decimal? PtEstimado { get; set; }
        public int? IdTc { get; set; }
        public int? TeSem { get; set; }
        public string? NumCoti { get; set; }
        public bool? IbArmado { get; set; }
        public string? CodCliente { get; set; }
        public string? NumDeal { get; set; }
        public string? NumSrv { get; set; }
        public string? NumPry { get; set; }
        public string? IdCc { get; set; }
        public string? IdScC { get; set; }
        public string? IdSscC { get; set; }
        public string? Nota1 { get; set; }
        public string? Nota2 { get; set; }
        public string? Nota3 { get; set; }
        public string? Nota4 { get; set; }
        //public bool? Estado { get; set; }
    }
}
