using System;

namespace CDS_Models.DTOs
{
    public class ProductoDTO
    {
        public string? IdPrd { get; set; }
        public int? IdMca { get; set; }
        public string? CodCom1 { get; set; }
        public string? CodCom2 { get; set; }
        public string? CodCom3 { get; set; }
        public string? Descrip { get; set; }
        public string? NCorto { get; set; }
        public int? IdCls { get; set; }
        public int? IdSCls { get; set; }
        public int? IdSsCls { get; set; }
        public string? IdCc { get; set; }
        public string? IdScC { get; set; }
        public string? IdSscC { get; set; }
        public bool? Estado { get; set; }
    }
}
