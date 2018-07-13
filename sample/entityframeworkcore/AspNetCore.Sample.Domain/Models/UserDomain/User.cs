using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Domain.Models.UserDomain
{
    public class User : IAggregateRoot
    {
        public User(string username, int point)
        {
            Username = username;
            Point = point;
        }

        public User(int id, string username, int point)
        {
            Id = id;
            Username = username;
            Point = point;
        }

        public object Id { get; private set; }

        public string Username { get; private set; }

        public int Point { get; private set; }
    }
}