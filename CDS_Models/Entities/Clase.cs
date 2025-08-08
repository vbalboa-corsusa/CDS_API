using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Clase
    {
        [Key]
        public int? IdClase { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }
        
        // Relación uno a muchos con Producto
        public ICollection<Producto>? Productos { get; set; }
    }
}
