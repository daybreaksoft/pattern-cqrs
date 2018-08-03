using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IRepository
    {
        Task<object> FindAsync(object id);

        Task<object> FindAllAsync();

        Task InsertAsync(object entity);

        Task UpdateAsync(object entity);

        /// <summary>
        /// 删除一个已经存在的Entity
        /// </summary>
        /// <param name="key">Entity主键</param>
        /// <returns></returns>
        Task DeleteAsync(object key);
    }

    /// <summary>
    /// Repository接口
    /// </summary>
    /// <typeparam name="TEntity">Entity类型，必须继承Daybreaksoft.Pattern.CQRS.IEntity</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : IEntity
    {
        /// <summary>
        /// 根据Id获取Entity实例
        /// </summary>
        /// <param name="id">Entity主键</param>
        /// <returns></returns>
        new Task<TEntity> FindAsync(object id);

        /// <summary>
        /// 获取所有的Entity实例
        /// </summary>
        /// <returns></returns>
        new Task<IEnumerable<TEntity>> FindAllAsync();

        /// <summary>
        /// 插入一个新的Entity
        /// </summary>
        /// <param name="entity">Entity实例</param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// 修改一个已经存在的Entity
        /// </summary>
        /// <param name="entity">Entity实例</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        
    }
}
