using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("TipoDocsIdent")]
    public class TipoDocsIdent
    {
        [Key, Column("Id_TDI", Order = 1)]
        public int? IdTdi { get; set; }

        [StringLength(100)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }

        public bool? Estado { get; set; }

        public ICollection<Cliente>? Clientes { get; set; }
        public ICollection<Vendedor>? Vendedores { get; set; }
    }
}
