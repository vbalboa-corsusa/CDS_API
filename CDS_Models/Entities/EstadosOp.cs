using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    [Table("EstadosOP")]
    public class EstadosOp
    {
        [Key, Column("Id_EstOP")]
        public int? IdEstOp { get; set; }

        [StringLength(50)]
        public string? Descrip { get; set; }

        [StringLength(10)]
        public string? NCorto { get; set; }
        //public bool? Estado { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
