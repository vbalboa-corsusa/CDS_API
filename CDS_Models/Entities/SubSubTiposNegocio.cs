using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class SubSubTiposNegocio
    {
        [Key]
        public int IdSstn { get; set; }
        public int? IdStn { get; set; }

        [StringLength(100)]
        public string? DescripcionSstn { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdStn")]
        public SubTiposNegocio? SubTiposNegocio { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
