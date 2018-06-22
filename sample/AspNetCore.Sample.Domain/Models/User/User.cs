using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class User : DefaultAggregateRoot
    {
        public User(IEventBus eventBus) : base(eventBus)
        {
        }

        public string Username { get; set; }

        public int Point { get; set; }

        //public virtual UserRole[] Roles { get; set; }

        #region Behaviors

        public void DeductPoint(int point)
        {
            this.Point = this.Point - point;

            this.ModifyAsync();
        }

        #endregion
    }
}