using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class Producto
    {
        [Key]
        public int IdProd { get; set; }
        public int? IdMarca { get; set; }

        [StringLength(50)]
        public string? CodComercial1 { get; set; }

        [StringLength(50)]
        public string? CodComercial2 { get; set; }

        [StringLength(50)]
        public string? CodComercial3 { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        [StringLength(10)]
        public string? NomCorto { get; set; }
        public int? IdClase { get; set; }
        public int? IdSClase { get; set; }
        public int? IdSSClase { get; set; }
        public int? IdCc { get; set; }
        public int? IdScc { get; set; }
        public int? IdSscc { get; set; }
        public bool? Estado { get; set; }
        public ICollection<ProdUm>? ProdUm { get; set; }
        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }

        [ForeignKey("IdMarca")]
        public Marca? Marca { get; set; }
        [ForeignKey("IdClase")]
        public Clase? Clase { get; set; }
        [ForeignKey("IdSClase")]
        public SubClase? SubClase { get; set; }
        [ForeignKey("IdSSClase")]
        public SubSubClase? SubSubClase { get; set; }
        [ForeignKey("IdCc")]
        public CCosto? CCosto { get; set; }
        [ForeignKey("IdScc")]
        public ScCosto? ScCosto { get; set; }
        [ForeignKey("IdSscc")]
        public SscCosto? SscCosto { get; set; }
    }
}
