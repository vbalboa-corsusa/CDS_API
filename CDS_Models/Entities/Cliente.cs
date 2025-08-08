using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models.Entities
{
    [Table ("Cliente")]
    public class Cliente
    {
        [Key, Column("Id_Clt", Order = 0)]
        [StringLength(10)]
        public string? IdClt { get; set; }

        [Column("Id_TDI", Order = 1)]
        public int? IdTdi { get; set; }

        [StringLength(20)]
        public string? NDoc { get; set; }

        [StringLength(100)]
        public string? RazonSocial { get; set; }

        [StringLength(100)]
        public string? CorreoClt { get; set; }

        [StringLength(50)]
        public string? TelefClt { get; set; }

        [StringLength(200)]
        public string? DirecClt { get; set; }

        [Column("Ib_CltPrv")]
        public bool? IbCltPrv { get; set; }

        [Column("Ib_CltFinal")]
        public bool? IbCltFinal { get; set; }

        public bool? Estado { get; set; }

        [ForeignKey(nameof(IdTdi))]
        public virtual TipoDocsIdent? TipoDocumento { get; set; }

        public virtual ICollection<OrdenPedido>? OrdenPedido { get; set; }
    }
}
