using System;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserAppService : AbstractDomainAppService<UserModel, UserEntity>
    {
        public UserAppService(IRepository<UserEntity> repository) : base(repository)
        {
        }

        public override async Task InsertAsync(UserModel aggregate)
        {
            await CheckUsernameUnique(aggregate);

            await base.InsertAsync(aggregate);
        }

        public override async Task UpdateAsync(UserModel aggregate)
        {
            await CheckUsernameUnique(aggregate);

            await base.UpdateAsync(aggregate);
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

        protected override void CopyValueToEntity(UserEntity entity, UserModel aggregate)
        {
            entity.Username = aggregate.Username;
            entity.Point = aggregate.Point;
        }

        protected override UserModel ConvertToAggregate(UserEntity entity)
        {
            return new UserModel(entity.Id, entity.Username, entity.Point);
        }
    }
}