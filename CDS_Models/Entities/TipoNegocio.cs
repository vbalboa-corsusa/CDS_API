using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class TipoNegocio
    {
        [Key, Column("Id_TN")]
        public int? IdTn { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Descrip { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? NCorto { get; set; }

        public bool? Estado { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
        public ICollection<SubTipoNegocio>? SubTiposNegocio { get; set; }
    }
}
