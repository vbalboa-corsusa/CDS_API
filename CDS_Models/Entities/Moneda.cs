using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Moneda
    {
        [Key]
        public int IdMda { get; set; }

        [StringLength(20)]
        public string? Nombre { get; set; }

        [StringLength(5)]
        public string? Equiv_Sunat { get; set; }
        public bool? Estado { get; set; }
    }
}
