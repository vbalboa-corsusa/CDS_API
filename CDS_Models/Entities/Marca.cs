using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("Marca")]
    public class Marca
    {
        [Key, Column("Id_Mca")]
        public int? IdMca { get; set; }

        [StringLength(50)]
        public string? NomMarca { get; set; }

        [StringLength(100)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}
