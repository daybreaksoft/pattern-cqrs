using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public int Point { get; private set; }
    }
}
