namespace CDS_Models.DTOs
{
    public class ProyectoDTO
    {
        public int IdProy { get; set; }
        public string? CodComercial { get; set; }
        public string? Descripcion { get; set; }
        public string? NomCorto { get; set; }
        public string? IdCc { get; set; }
        public string? IdScc { get; set; }
        public string? IdSscc { get; set; }
        public bool? Estado { get; set; }
    }
}
