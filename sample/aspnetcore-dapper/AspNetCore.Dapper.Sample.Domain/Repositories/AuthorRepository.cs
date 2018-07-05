using System.Data;
using AspNetCore.Dapper.Sample.Data.Entities;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Extensions.Dapper;

namespace AspNetCore.Dapper.Sample.Domain.Repositories
{
    public class AuthorRepository : AbstractRepository<AuthorAggregate, AuthorEntity>
    {
        public AuthorRepository(IDbConnection connection, IAggregateBus aggregateBus) : base(connection, aggregateBus)
        {
        }
    }
}
