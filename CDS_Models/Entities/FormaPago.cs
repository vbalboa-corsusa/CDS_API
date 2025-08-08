using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models.Entities
{
    [Table("FormaPago")]
    public class FormaPago
    {
        [Key, Column("Id_FP")]
        public int? IdFp { get; set; }
        public int? IdCfp { get; set; }

        [StringLength(100)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }
        
        public bool? Estado { get; set; }

        public ICollection<OrdenPedido>? OrdenesPedido { get; set; }
    }
}
