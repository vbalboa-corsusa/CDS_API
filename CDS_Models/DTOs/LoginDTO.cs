using System.ComponentModel.DataAnnotations;

namespace CDS_Models.DTOs
{
    public class LoginDTO
    {
        public int IdLogin { get; set; }

        [Required]
        [StringLength(30)]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Pass { get; set; } = string.Empty;
        
        public bool Estado { get; set; } = true;
    }
} 