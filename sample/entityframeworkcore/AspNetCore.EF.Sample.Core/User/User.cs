using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.User
{
    [Table("Users")]
    public class UserModel : IAggregateRoot, IEntity
    {
        public UserModel(string username, int point) : this(0, username, point)
        {
        }

        public UserModel(int id, string username, int point)
        {
            Id = id;
            Username = username;
            Point = point;
        }

        object IAggregateRoot.Id => Id;

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public int Point { get; set; }
    }
}