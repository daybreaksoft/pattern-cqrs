using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Sample.Domain.Repository
{
    public class UserRepository : DefaultRepository<User>
    {
        public UserRepository(DbContext db) : base(db)
        {
        }

        //public async override Task InsertAsync(User entity)
        //{
        //    await base.InsertAsync(entity);

        //    foreach (var role in entity.Roles)
        //    {
        //        role.UserId = entity.UserId;

        //        await Db.Set<UserRole>().AddAsync(role);
        //    }
        //}
    }
}
