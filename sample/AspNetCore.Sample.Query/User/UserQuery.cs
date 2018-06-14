using AspNetCore.Sample.Repository;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Query.User
{
    public class UserQuery : QueryBase<SampleDbContext>
    {
        public UserQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<UserListItemViewModel>> GetUsers()
        {
            var users = await Db.Users.ToListAsync();

            return users.Select(UserViewModelTransfer<UserListItemViewModel>);
        }

        #region Private Helper

        private T UserViewModelTransfer<T>(Repository.Entities.User user) where T : new()
        {
            var userViewModel = new T();

            user.CopyValueTo(userViewModel);

            return userViewModel;
        }

        #endregion
    }
}
