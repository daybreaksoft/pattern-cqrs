﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// Repository接口
    /// </summary>
    /// <typeparam name="TEntity">Entity类型，必须继承Daybreaksoft.Pattern.CQRS.IEntity</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 根据Id获取Entity实例
        /// </summary>
        /// <param name="id">Entity主键</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object id);

        /// <summary>
        /// 获取所有的Entity实例
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> FindAllAsync();

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

        /// <summary>
        /// 删除一个已经存在的Entity
        /// </summary>
        /// <param name="key">Entity主键</param>
        /// <returns></returns>
        Task DeleteAsync(object key);
    }
}
