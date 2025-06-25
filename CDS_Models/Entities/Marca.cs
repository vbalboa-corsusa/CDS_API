using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }

        [StringLength(100)]
        public string? NombreMarca { get; set; }

        [StringLength(100)]
        public string? DescripcionMarca { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}
