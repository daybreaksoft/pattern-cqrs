using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data;
using AspNetCore.EF.Sample.Data.Entities;
using AspNetCore.EF.Sample.Query.ViewModels;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Query
{
    public class UserQuery : AbstractQuery<SampleDbContext>
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

        private T UserViewModelTransfer<T>(UserEntity user) where T : new()
        {
            var userViewModel = new T();

            user.CopyValueTo(userViewModel);

            return userViewModel;
        }

        #endregion
    }
}
