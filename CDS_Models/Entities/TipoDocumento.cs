using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class TipoDocumento
    {
        [Key]
        public int IdTdi { get; set; }

        [StringLength(100)]
        public string? DescripcionTdi { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Cliente>? Clientes { get; set; }
        public ICollection<Vendedor>? Vendedor { get; set; }
    }
}
