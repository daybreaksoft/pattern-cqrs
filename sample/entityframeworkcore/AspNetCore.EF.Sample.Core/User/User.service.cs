using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserService : AbstractApplicationService<UserModel, UserEntity>
    {
        public UserService(IRepository<UserEntity> repository) : base(repository)
        {
        }

        protected override async Task BeforeInsertAsync(UserModel aggregate)
        {
            await CheckUsernameUnique(aggregate);
        }

        protected override async Task BeforeUpdateAsync(UserModel aggregate)
        {
            await CheckUsernameUnique(aggregate);
        }

        #region Constraint

        private async Task CheckUsernameUnique(UserModel aggregate)
        {
            var queryable = Repository.GetQueryable();

            var id = Convert.ToInt32(aggregate.Id);

            if (await queryable.Where(p => p.Username == aggregate.Username && p.Id != id).AnyAsync())
            {
                throw new Exception($"Username {aggregate.Username} already exists.");
            }
        }

        #endregion

        #region Data Transfer

        protected override void CopyValueToEntity(UserEntity entity, UserModel aggregate)
        {
            entity.Username = aggregate.Username;
            entity.Point = aggregate.Point;
        }

        protected override UserModel ConvertToAggregate(UserEntity entity)
        {
            return new UserModel(entity.Id, entity.Username, entity.Point);
        }

        #endregion
    }
}