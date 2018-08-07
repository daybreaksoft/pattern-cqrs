using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserRepository : DefaultRepository<UserEntity>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task PersistUpdateOf(object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var user = (UserEntity)entity;

            // Get unmodified entity from database
            var unmodifiedEntity = await FindAsync(user.Id);

            // Copy values from entity to unmodified entity
            entity.CopyValueTo(unmodifiedEntity);

            // Remove roles if not selected
            foreach (var unmodifiedUserRole in unmodifiedEntity.Roles)
            {
                if (user.Roles.All(p => p.Role != unmodifiedUserRole.Role))
                {
                    Db.Set<UserRoleEntity>().Remove(unmodifiedUserRole);
                }
            }

            // Insert roles
            foreach (var userRole in user.Roles)
            {
                if (unmodifiedEntity.Roles.All(p => p.Role != userRole.Role))
                {
                    userRole.UserId = user.Id;

                    await Db.Set<UserRoleEntity>().AddAsync(userRole);
                }
            }
        }

        public override async Task PersistDeleteOf(object entity)
        {
            var user = (UserEntity)entity;

            // Get unmodified entity from database
            var unmodifiedEntity = await FindAsync(user.Id);

            Db.Set<UserRoleEntity>().RemoveRange(unmodifiedEntity.Roles);
            Db.Set<UserEntity>().Remove(unmodifiedEntity);
        }

        public override async Task<UserEntity> FindAsync(object id)
        {
            var user = await base.FindAsync(id);

            user.Roles = await Db.Set<UserRoleEntity>().Where(p => p.UserId == Convert.ToInt32(id)).ToListAsync();

            return user;
        }
    }
}
