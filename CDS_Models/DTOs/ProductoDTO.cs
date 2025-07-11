using System;

namespace CDS_Models.DTOs
{
    public class ProductoDTO
    {
        public int IdProd { get; set; }
        public int? IdMarca { get; set; }
        public string? CodComercial1 { get; set; }
        public string? CodComercial2 { get; set; }
        public string? CodComercial3 { get; set; }
        public string? Descripcion { get; set; }
        public string? NomCorto { get; set; }
        public int? IdClase { get; set; }
        public int? IdSClase { get; set; }
        public int? IdSSClase { get; set; }
        public string? IdCc { get; set; }
        public string? IdScc { get; set; }
        public string? IdSscc { get; set; }
        public bool? Estado { get; set; }
    }
}
