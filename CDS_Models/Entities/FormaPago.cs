using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models.Entities
{
    public class FormaPago
    {
        [Key]
        public int IdFp { get; set; }
        public int? IdCfp { get; set; }
        [StringLength(100)]
        public string? DescripcionFp { get; set; }
        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<OrdenPedido>? OrdenesPedido { get; set; }
    }
}
