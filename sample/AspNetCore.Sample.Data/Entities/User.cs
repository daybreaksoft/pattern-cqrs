using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Data.Entities
{
    [Table("Users")]
    public class UserEntity : IEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public int Point { get; set; }

        public virtual ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
