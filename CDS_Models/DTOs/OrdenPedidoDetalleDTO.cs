using System;

namespace CDS_Models.DTOs
{
    public class OrdenPedidoDetalleDTO
    {
        public int IdOpd { get; set; }
        public int? IdStatus { get; set; }
        public int? IdTn { get; set; }
        public int? IdStn { get; set; }
        public int? IdSstn { get; set; }
        public int? IdProd { get; set; }
        public int? IdServ { get; set; }
        public int? IdProy { get; set; }
        public string? ItemOp { get; set; }
        public string? CodComercial { get; set; }
        public decimal? Cantidad { get; set; }
        public int? IdUm { get; set; }
        public int? IdMda { get; set; }
        public decimal? Pvu { get; set; }
        public DateTime? FecReqCli { get; set; }
        public decimal? PtEstimado { get; set; }
        public int? IdTc { get; set; }
        public int? TeSem { get; set; }
        public string? NumCoti { get; set; }
        public bool? IbArmado { get; set; }
        public string? CodCliente { get; set; }
        public string? NumDeal { get; set; }
        public string? NumServicio { get; set; }
        public string? NumProyecto { get; set; }
        public int? IdCc { get; set; }
        public int? IdScc { get; set; }
        public int? IdSscc { get; set; }
        public string? Nota1 { get; set; }
        public string? Nota2 { get; set; }
        public string? Nota3 { get; set; }
        public string? Nota4 { get; set; }
        public bool? Estado { get; set; }
    }
}
