using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Entities;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS.Extensions.Dapper;

namespace AspNetCore.Dapper.Sample.Query
{
    public class AuthorQuery : AbstractQuery
    {
        public AuthorQuery(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<AuthorListItemViewModel>> GetAuthors()
        {
            var collection = await Connection.GetAllAsync<AuthorEntity>();

            return collection.Select(p => new AuthorListItemViewModel
            {
                Id = p.Id,
                Name = p.Name
            });
        }
    }
}
