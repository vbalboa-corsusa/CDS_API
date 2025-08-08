using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class SubSubClase : SubClase
    {
        public int? IdSSClase { get; set; }

        [StringLength(50)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Producto>? ProductosSubSubClase { get; set; }
    }
}
