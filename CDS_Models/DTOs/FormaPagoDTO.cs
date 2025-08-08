namespace CDS_Models.DTOs
{
    public class FormaPagoDTO
    {
        public int IdFp { get; set; }
        public int? IdCfp { get; set; }
        public string? Descrip { get; set; }
        public string? NCorto { get; set; }
        public bool? Estado { get; set; }
    }
}
