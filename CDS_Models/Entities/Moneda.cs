using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Moneda
    {
        [Key]
        public int IdMda { get; set; }

        [StringLength(20)]
        public string? Nombre { get; set; }

        [StringLength(5)]
        public string? EquivSunat { get; set; }
        public bool? Estado { get; set; }

        public ICollection<CDS_Models.Entities.OrdenPedido>? OrdenPedido { get; set; }
        public ICollection<TcUsd>? TcUsd { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
