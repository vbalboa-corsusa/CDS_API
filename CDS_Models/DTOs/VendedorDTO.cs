using System;

namespace CDS_Models.DTOs
{
    public class VendedorDTO
    {
        public int IdVendedor { get; set; }
        public int? IdTdi { get; set; }
        public string? NumDocVendedor { get; set; }
        public string? NombreVendedor { get; set; }
        public bool? IbLider { get; set; }
        public bool? Estado { get; set; }
    }
}
