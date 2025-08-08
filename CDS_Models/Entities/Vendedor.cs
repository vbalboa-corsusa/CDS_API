using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Vendedor
    {
        [Key, Column("Id_Vdr", TypeName = "char(10)")]
        public string? IdVdr { get; set; }
        public int? IdTdi { get; set; }

        [StringLength(10)]
        public string? NDoc { get; set; }

        [StringLength(100)]
        public string? NomVdr { get; set; }

        [Column("Ib_Lider")]
        public bool? IbLider { get; set; }
        public bool? Estado { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedido>? OrdenPedido { get; set; }
        [ForeignKey("IdTdi")]
        public TipoDocsIdent? TipoDocumento { get; set; }
    }
}
