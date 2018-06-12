using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Repository.Entities
{
    public class User : IEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public int Point { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
