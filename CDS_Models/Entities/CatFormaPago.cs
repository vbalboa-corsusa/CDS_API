using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("Cat_Forma_Pago")]
    public class CatFormaPago
    {
        [Key, Column("Id_CFP")]
        public int IdCfp { get; set; }

        [StringLength(20)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<FormaPago>? FormaPago { get; set; }
    }
}
