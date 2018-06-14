using System.Threading.Tasks;
using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class UserModel : DefaultDomainModel<User>
    {
        public UserModel(IRepository<User> repository) : base(repository)
        {
        }

        public string Username { get; set; }

        public int Point { get; private set; }

        #region Behaviors

        public async Task DeductPointAsync(int point)
        {
            this.Point = this.Point - point;

            await this.UpdateAsync();
        }

        #endregion
    }
}
