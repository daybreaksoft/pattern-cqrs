using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserModel : IAggregateRoot, IAggregateRootSetKey
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

        #region IAggregateRoot Implementation

        public object Id { get; private set;}

        void IAggregateRootSetKey.SetKey(object id)
        {
            Id = id;
        }

        #endregion

        public string Username { get; set; }

        public int Point { get; set; }
    }
}
