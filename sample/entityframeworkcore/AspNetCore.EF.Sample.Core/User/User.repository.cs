using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserRepository:DefaultRepository<UserEntity>
    {
        public UserRepository(DbContext db) : base(db)
        {
        }

        public override async Task InsertAsync(UserEntity entity)
        {
            await base.InsertAsync(entity);


        }
    }
}
