namespace CDS_Models.DTOs
{
    public class ClienteDTO
    {
        public string? IdClt { get; set; }
        public int? IdTdi { get; set; }
        public string? NDoc { get; set; }
        public string? RazonSocial { get; set; }
        public string? CorreoClt { get; set; }
        public string? TelefClt { get; set; }
        public string? DirecClt { get; set; }
        public bool? IbCltPrv { get; set; }
        public bool? IbCltFinal { get; set; }
        public bool? Estado { get; set; }
    }
}
