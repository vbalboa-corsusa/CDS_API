using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Moneda
    {
        [Key, Column("Id_Mda")]
        public int? IdMda { get; set; }

        [StringLength(20)]
        public string? Nombre { get; set; }

        [Column("Equiv_Sunat"), StringLength(5)]
        public string? EquivSunat { get; set; }
        public bool? Estado { get; set; }
    }
}
