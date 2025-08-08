using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("Prod_UM")]
    public class ProdUm
    {
        [Key, Column("Id_Prd", Order = 0)]
        public string? IdPrd { get; set; }

        [Key, Column("Id_UM", Order = 1)]
        public int IdUm { get; set; }

        [StringLength(100)]
        public string? Descrip { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey(nameof(IdPrd))]
        public Producto? Productos { get; set; }

        [ForeignKey(nameof(IdUm))]
        public UnidadMedida? UnidadesMedida { get; set; }
    }
}
