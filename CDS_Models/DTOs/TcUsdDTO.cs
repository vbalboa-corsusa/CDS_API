using System;

namespace CDS_Models.DTOs
{
    public class TcUsdDTO
    {
        public int IdTc { get; set; }
        public int? IdMda { get; set; }
        public DateTime? FechaTc { get; set; }
        public decimal? TipCam { get; set; }
        // Add other properties as needed, matching the TcUsd model
    }
}
