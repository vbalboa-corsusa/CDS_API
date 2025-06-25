using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        public int? IdTdi { get; set; }

        [StringLength(100)]
        public string? RazonSocial { get; set; }

        [StringLength(100)]
        public string? CorreoCliente { get; set; }

        [StringLength(15)]
        public string? NumDocumento { get; set; }

        [StringLength(50)]
        public string? TelefonoCliente { get; set; }

        [StringLength(200)]
        public string? DireccionCliente { get; set; }
        public bool? IbCltPrv { get; set; }
        public bool? IbCltFinal { get; set; }

        [ForeignKey("IdTdi")]
        public TipoDocumento? TipoDocumento { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedido>? OrdenPedido { get; set; }
    }
}
