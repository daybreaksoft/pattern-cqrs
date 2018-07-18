using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.User
{
    public class UserMap : IDataMap<UserModel, UserEntity>
    {
        public UserModel ConvertToAggregate(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
