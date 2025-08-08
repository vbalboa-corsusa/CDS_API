using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("TipNeg_Sub")]
    public class SubTipoNegocio
    {
        [Key, Column("Id_STN")]
        public int? IdStn { get; set; }

        [Column("Id_TN")]
        public int? IdTn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Descrip { get; set; }

        [Column(TypeName = "nvarcar(10)")]
        public string? NCorto { get; set; }

        public bool? Estado { get; set; }

        [ForeignKey("IdTn")]
        public TipoNegocio? TipoNegocio { get; set; }

        public ICollection<SubSubTipoNegocio>? SubSubTipoNegocio { get; set; }
        
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
