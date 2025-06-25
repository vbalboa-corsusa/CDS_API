using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class CatFormaPago
    {
        [Key]
        public int IdCfp { get; set; }

        [StringLength(100)]
        public string? DescripcionCfp { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<FormaPago>? FormaPago { get; set; }
    }
}
