using System.Collections.Generic;
using AspNetCore.EF.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserModel : IAggregateRoot, IAggregateRootSetKey
    {
        public UserModel(string username, int point, ICollection<UserRoleConst> roles) : this(0, username, point, roles)
        {
        }

        public UserModel(int id, string username, int point, ICollection<UserRoleConst> roles)
        {
            Id = id;
            Username = username;
            Point = point;
            Roles = roles ?? new List<UserRoleConst>();
        }

        #region IAggregateRoot Implementation

        public object Id { get; private set; }

        void IAggregateRootSetKey.SetKey(object id)
        {
            Id = id;
        }

        #endregion

        public string Username { get; set; }

        public int Point { get; set; }

        public ICollection<UserRoleConst> Roles { get; set; }
    }
}
