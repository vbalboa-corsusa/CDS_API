using CDS_Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class StatusOp
    {
        [Key]
        public int IdStatus { get; set; }

        [StringLength(100)]
        public string? DescripcionStatus { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
