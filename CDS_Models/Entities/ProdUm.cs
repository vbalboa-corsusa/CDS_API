using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class ProdUm
    {
        [Key]
        [Column(Order = 0)]
        public int IdProd { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IdUm { get; set; }

        [StringLength(100)]
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdProd")]
        public Producto? Productos { get; set; }
        [ForeignKey("IdUm")]
        public UnidadMedida? UnidadesMedida { get; set; }
    }
}
