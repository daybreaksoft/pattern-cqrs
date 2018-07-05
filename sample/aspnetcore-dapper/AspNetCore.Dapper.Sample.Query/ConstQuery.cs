using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Const;
using AspNetCore.Dapper.Sample.Data.Entities;
using Dapper;
using Daybreaksoft.Pattern.CQRS.Extensions.Dapper;

namespace AspNetCore.Dapper.Sample.Query
{
    public class ConstQuery : AbstractQuery
    {
        public ConstQuery(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<ConstEntity>> GetSelectItems(ConstCategoryConst category)
        {
            var sql = "SELECT Id, DisplayText FROM Const WHERE CategoryId = @CategoryId AND Enabled = @Enabled";

            var collection = await Connection.QueryAsync<ConstEntity>(sql, new { CategoryId = (int)category, Enabled = true });

            return collection;
        }
    }
}
