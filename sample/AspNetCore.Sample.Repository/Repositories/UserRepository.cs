using AspNetCore.Sample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;

namespace AspNetCore.Sample.Repository.Repositories
{
    public class UserRepository : DefaultRepository<User>
    {
        public UserRepository(IDbContext db) : base(db)
        {
        }
    }
}
