using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Domain.Models.UserDomain
{
    public class UserAppService : IDomainService<User>
    {
        protected readonly IRepository<UserEntity> UserRepository;

        public UserAppService(IRepository<UserEntity> userRepository)
        {
            UserRepository = userRepository;
        }

        public Task<User> FindAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertAsync(User aggregate)
        {
            await UserRepository.InsertAsync(ConvetToEntity(aggregate));
        }

        public Task UpdateAsync(User aggregate)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(User aggregate)
        {
            throw new System.NotImplementedException();
        }

        #region Data Transfer

        public UserEntity ConvetToEntity(User user)
        {
            return new UserEntity
            {
                Id = Convert.ToInt32(user.Id),
                Username = user.Username,
                Point = user.Point
            };
        }

        #endregion
    }
}