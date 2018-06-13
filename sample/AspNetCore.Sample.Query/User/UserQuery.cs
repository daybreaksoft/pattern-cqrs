using AspNetCore.Sample.Repository;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Query.User
{
    public class UserQuery : QueryBase<SampleDbContext>
    {
        public UserQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IList<UserViewModel>> GetUsers()
        {
            var users = await Db.Users.ToListAsync();

            return users.Select(UserViewModelTransfer).ToList();
        }

        private UserViewModel UserViewModelTransfer(Repository.Entities.User user)
        {
            var userViewModel = new UserViewModel();

            user.CopyValueTo(userViewModel);

            return userViewModel;
        }
    }
}
