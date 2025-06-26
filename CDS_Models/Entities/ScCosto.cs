using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class ScCosto : CCosto
    {
        [StringLength(16)]
        public string IdScc { get; set; }

        public ICollection<SscCosto>? SscCosto { get; set; }
        public ICollection<Producto>? ProductosScCosto { get; set; }
        public ICollection<Servicio>? ServiciosScCosto { get; set; }
        public ICollection<Proyecto>? ProyectosScCosto { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetallesScCosto { get; set; }
    }
}
