using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDS_Models
{
    public class Login
    {
        [Key]
        public int IdLogin { get; set; }

        [StringLength(30)]
        public string? Usuario { get; set; }

        [StringLength(30)]
        public string? Pass { get; set; }
        
        public bool? Estado { get; set; }
    }
} 