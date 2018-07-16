using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserAppService : IDomainAppService<UserModel>
    {
        protected readonly IRepository<UserEntity> UserRepository;

        public UserAppService(IRepository<UserEntity> userRepository)
        {
            UserRepository = userRepository;
        }

        public Task<UserModel> FindAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> FindAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertAsync(UserModel aggregate)
        {
            var queryable = UserRepository.GetQueryable();

            if (await queryable.Where(p => p.Username == aggregate.Username).AnyAsync())
            {
                throw new Exception($"Username {aggregate.Username} already exists.");
            }

            await UserRepository.InsertAsync(ConvetToEntity(aggregate));
        }

        public Task UpdateAsync(UserModel aggregate)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(UserModel aggregate)
        {
            throw new System.NotImplementedException();
        }

        #region Data Transfer

        public UserEntity ConvetToEntity(UserModel userModel)
        {
            return new UserEntity
            {
                Id = Convert.ToInt32(userModel.Id),
                Username = userModel.Username,
                Point = userModel.Point
            };
        }

        #endregion
    }
}