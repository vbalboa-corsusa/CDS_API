using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class SubTiposNegocio
    {
        [Key]
        public int IdStn { get; set; }
        public int? IdTn { get; set; }

        [StringLength(100)]
        public string? DescripcionStn { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }
        [ForeignKey("IdTn")]
        public TiposNegocio? TiposNegocio { get; set; }
        public ICollection<SubSubTiposNegocio>? SubSubTiposNegocio { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
