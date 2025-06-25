namespace CDS_Models.DTOs
{
    public class SubTipoNegocioDTO
    {
        public int IdStn { get; set; }
        public int? IdTn { get; set; }
        public string? DescripcionStn { get; set; }
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }
    }
}
