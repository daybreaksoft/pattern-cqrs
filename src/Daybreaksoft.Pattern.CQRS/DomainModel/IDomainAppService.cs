using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型，必须继承Daybreaksoft.Pattern.CQRS.IAggregateRoot</typeparam>
    public interface IDomainAppService<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> FindAsync(object id);

        Task<IEnumerable<TAggregateRoot>> FindAllAsync();

        Task InsertAsync(TAggregateRoot aggregate);

        Task UpdateAsync(TAggregateRoot aggregate);

        Task DeleteAsync(object id);

        Task DeleteAsync(TAggregateRoot aggregate);
    }
}
