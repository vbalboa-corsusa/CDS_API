using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using CDS_Models.Entities;

namespace CDS_Models
{
    public class TcUsd
    {
        [Key]
        public int IdTc { get; set; }
        public int? IdMda { get; set; }
        public DateTime? FechaTc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TipCam { get; set; }

        [ForeignKey("IdMda")]
        public Moneda? Moneda { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
