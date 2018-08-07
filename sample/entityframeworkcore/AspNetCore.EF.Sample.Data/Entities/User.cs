using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    [Table("Users")]
    public class UserEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; set; }
    }
}