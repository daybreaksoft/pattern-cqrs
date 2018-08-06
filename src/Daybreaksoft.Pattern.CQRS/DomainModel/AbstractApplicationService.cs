using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public abstract class AbstractApplicationService<TAggregateRoot, TEntity> : IApplicationService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
        where TEntity : class, IEntity, new()
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IRepository<TEntity> Repository;

        protected AbstractApplicationService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public virtual async Task<TAggregateRoot> FindAsync(object id)
        {
            return ConvertToAggregate(await Repository.FindAsync(id));
        }

        public virtual async Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            var collection = await Repository.FindAllAsync();

            return collection.Select(ConvertToAggregate);
        }

        public virtual async Task InsertAsync(TAggregateRoot aggregate, bool immediate)
        {
            UnitOfWork.RegisterdModels.Add(new RegisterdModel
            {
                Model = aggregate,
                Action = RegisterAction.Add,
                PersistService = this
            });

            if (immediate)
            {
                await UnitOfWork.CommitAsync();
            }
        }

        public virtual void Update(TAggregateRoot aggregate)
        {
            UnitOfWork.RegisterdModels.Add(new RegisterdModel
            {
                Model = aggregate,
                Action = RegisterAction.Modify,
                PersistService = this
            });
        }

        public virtual void Delete(object id)
        {
            throw new NotSupportedException();
            //return Repository.DeleteAsync(id);
        }

        public virtual async Task PersistInsertAsync(object obj)
        {
            var aggregate = (TAggregateRoot)obj;

            await BeforeInsertAsync(aggregate);

            var newEntity = new TEntity();

            CopyValueToEntity(newEntity, aggregate);

            await Repository.InsertAsync(newEntity);

            // Get id of entity after added
            var keyProperty = newEntity.GetType().FindProperty<KeyAttribute>();
            var id = keyProperty.GetValue(newEntity);

            // Set id value for aggregate
            if (aggregate is IAggregateRootSetKey key)
            {
                key.SetKey(id);
            }
            else
            {
                throw new Exception($"The {aggregate.GetType().FullName} does not inherit {typeof(IAggregateRootSetKey).FullName}.");
            }
        }

        public virtual async Task PersistUpdateAsync(object obj)
        {
            var aggregate = (TAggregateRoot) obj;

            await BeforeUpdateAsync(aggregate);

            var unModifiedEntity = await Repository.FindAsync(aggregate.Id);

            CopyValueToEntity(unModifiedEntity, aggregate);

            await Repository.UpdateAsync(unModifiedEntity);
        }

        public virtual Task PersistDeleteAsync(TAggregateRoot aggregate)
        {
            return Repository.DeleteAsync(aggregate.Id);
        }

        protected virtual async Task BeforeInsertAsync(TAggregateRoot aggregate)
        {
            await Task.FromResult(0);
        }

        protected virtual async Task BeforeUpdateAsync(TAggregateRoot aggregate)
        {
            await Task.FromResult(0);
        }

        #region Data Transfer

        protected abstract void CopyValueToEntity(TEntity entity, TAggregateRoot aggregate);

        protected abstract TAggregateRoot ConvertToAggregate(TEntity entity);

        #endregion
    }
}
