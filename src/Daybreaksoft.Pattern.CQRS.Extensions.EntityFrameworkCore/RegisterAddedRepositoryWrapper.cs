using Daybreaksoft.Pattern.CQRS.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public class RegisterAddedRepositoryWrapper
    {
        public RegisterAddedRepositoryWrapper(IRepository repository, Action<IEntity> setKeyAction)
        {
            Repository = repository;
            SetKeyAction = setKeyAction;
        }

        public IRepository Repository { get; private set; }

        public Action<IEntity> SetKeyAction { get; private set; }
    }
}
