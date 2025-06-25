using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class SubClase : Clase
    {
        public int IdSClase { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Producto>? ProductosSubClase { get; set; }
        public ICollection<SubSubClase>? SubSubClase { get; set; }
    }
}
