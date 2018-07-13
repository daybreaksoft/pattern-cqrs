using System;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultRepositoryFactory : IRepositoryFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultRepositoryFactory(IDependencyInjection di)
        {
            DI = di;
        }

        public virtual Type GetRepositoryType(IEntity entity)
        {
            return GetRepositoryType(entity.GetType());
        }

        public virtual Type GetRepositoryType(Type entityType)
        {
            return typeof(IRepository<>).MakeGenericType(entityType);
        }

        public virtual object GetRepository(Type repositoryType)
        {
            return DI.GetService(repositoryType);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
        {
            return DI.GetService<IRepository<TEntity>>();
        }
    }
}
