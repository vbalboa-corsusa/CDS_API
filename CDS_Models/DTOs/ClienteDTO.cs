namespace CDS_Models.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public int? IdTdi { get; set; }
        public string? RazonSocial { get; set; }
        public string? CorreoCliente { get; set; }
        public string? NumDocumento { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public bool? IbCltPrv { get; set; }
        public bool? IbCltFinal { get; set; }
    }
}
