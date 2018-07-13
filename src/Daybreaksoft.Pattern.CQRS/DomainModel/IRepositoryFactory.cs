using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IRepositoryFactory
    {
        Type GetRepositoryType(IEntity entity);

        Type GetRepositoryType(Type entityType);
        
        object GetRepository(Type repositoryType);

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity;
    }
}
