﻿using System.Collections;
using System.Collections.Generic;
using AspNetCore.EF.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class UserEntity : IAggregateRoot, IEntity
    {
        public UserEntity(string username, int point, UserRoleConst[] roles) : this(0, username, point, roles)
        {
        }

        public UserEntity(int id, string username, int point, UserRoleConst[] roles)
        {
            Id = id;
            Username = username;
            Point = point;
            Roles = new List<UserRoleEntity>();
        }

        object IAggregateRoot.Id => Id;

        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; set; }
    }
}