using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCore.EF.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    [Table("UserRoles")]
    public class UserRoleEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserRoleConst Role { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
