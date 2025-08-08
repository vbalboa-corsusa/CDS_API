using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("TipoNeg_SubSub")]
    public class SubSubTipoNegocio
    {
        [Key, Column("Id_SSTN")]
        public int? IdSstn { get; set; }

        [Column("Id_STN")]
        public int? IdStn { get; set; }

        [Column("Id_TN")]
        public int? IdTn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Descrip { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? NCorto { get; set; }

        public bool? Estado { get; set; }

        [ForeignKey("IdStn")]
        public SubTipoNegocio? SubTipoNegocio { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
