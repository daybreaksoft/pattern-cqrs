using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Sample.Domain.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }
}
