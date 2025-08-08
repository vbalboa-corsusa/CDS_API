using System;

namespace CDS_Models.DTOs
{
    public class VendedorDTO
    {
        public string? IdVdr { get; set; }
        public int? IdTdi { get; set; }
        public string? NDoc { get; set; }
        public string? NomVdr { get; set; }
        public bool? IbLider { get; set; }
        public bool? Estado { get; set; }
    }
}
