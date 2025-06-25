using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class TiposNegocio
    {
        [Key]
        public int IdTn { get; set; }

        [StringLength(200)]
        public string? DescripcionTn { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
        public ICollection<SubTiposNegocio>? SubTiposNegocio { get; set; }
    }
}
