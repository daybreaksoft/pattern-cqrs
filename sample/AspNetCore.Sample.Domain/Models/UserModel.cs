using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Sample.Repository.Entities;
using AspNetCore.Sample.Repository.Repositories;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class UserModel : DefaultDomainModel<User>
    {
        public UserModel(IRepository<User> repository) : base(repository)
        {
        }

        public UserModel(object id, IRepository<User> repository) : base(id, repository)
        {
        }

        public string Username { get; set; }

        public int Point { get; set; }
    }
}
