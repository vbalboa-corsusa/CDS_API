using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Vendedor
    {
        [Key]
        public int IdVendedor { get; set; }
        public int? IdTdi { get; set; }

        [StringLength(10)]
        public string? NumDocVendedor { get; set; }

        [StringLength(100)]
        public string? NombreVendedor { get; set; }
        public bool? IbLider { get; set; }
        public bool? Estado { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedido>? OrdenPedido { get; set; }
        [ForeignKey("IdTdi")]
        public TipoDocumento? TipoDocumento { get; set; }
    }
}
