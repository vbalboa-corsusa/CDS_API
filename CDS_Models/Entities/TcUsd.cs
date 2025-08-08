using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using CDS_Models.Entities;

namespace CDS_Models
{
    [Table("Tc_USD")]
    public class TcUsd
    {
        [Key, Column("ID_TC")]
        public int? IdTc { get; set; }

        [Column("Id_Mda")]
        public int? IdMda { get; set; }

        [Column("FechaTC")]
        public DateTime? FechaTc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TipCam { get; set; }

        [ForeignKey("IdMda")]
        public Moneda? Moneda { get; set; }

        public ICollection<OrdenPedidoDetalle>? OrdenPedidoDetalle { get; set; }
    }
}
