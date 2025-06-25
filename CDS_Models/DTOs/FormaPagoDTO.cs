namespace CDS_Models.DTOs
{
    public class FormaPagoDTO
    {
        public int IdFp { get; set; }
        public int? IdCfp { get; set; }
        public string? DescripcionFp { get; set; }
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }
    }
}
