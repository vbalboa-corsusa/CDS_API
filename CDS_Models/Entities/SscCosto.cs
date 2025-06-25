using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class SscCosto : ScCosto
    {
        public int IdSscc { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Proyecto>? ProyectosSscCosto { get; set; }
        public ICollection<Servicio>? ServiciosSscCosto { get; set; }
        public ICollection<Producto>? ProductosSscCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesSscCosto { get; set; }
    }
}
