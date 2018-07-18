using System.ComponentModel.DataAnnotations;
using AspNetCore.EF.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class UserRoleEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserRoleConst Role { get; set; }
    }
}
