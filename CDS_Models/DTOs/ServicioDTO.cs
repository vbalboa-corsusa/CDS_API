namespace CDS_Models.DTOs
{
    public class ServicioDTO
    {
        public int IdServ { get; set; }
        public string? CodComercial { get; set; }
        public string? Descripcion { get; set; }
        public string? NomCorto { get; set; }
        public int? IdCc { get; set; }
        public int? IdScc { get; set; }
        public int? IdSscc { get; set; }
        public bool? Estado { get; set; }
    }
}
