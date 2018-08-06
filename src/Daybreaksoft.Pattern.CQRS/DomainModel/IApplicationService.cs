using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{

    public interface IApplicationPersistService
    {
        Task PersistInsertAsync(object obj);

        Task PersistUpdateAsync(object obj);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型，必须继承Daybreaksoft.Pattern.CQRS.IAggregateRoot</typeparam>
    public interface IApplicationService<TAggregateRoot> : IApplicationPersistService where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> FindAsync(object id);

        Task<IEnumerable<TAggregateRoot>> FindAllAsync();

        Task InsertAsync(TAggregateRoot aggregate, bool immediate = false);

        void Update(TAggregateRoot aggregate);

        void Delete(object id);

      
    }
}
