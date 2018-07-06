using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Entities;
using AspNetCore.Dapper.Sample.Query.ViewModels;
using Dapper;
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
            var sql = @"SELECT a.Id, a.Name, c.DisplayText AS Sex FROM Authors a JOIN Const c ON c.Id = a.Sex";

            var collection = await Connection.QueryAsync(sql);

            return collection.Select(p => new AuthorListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Sex = p.Sex
            });
        }

        public async Task<IEnumerable<AuthorSelectItemViewModel>> GetAuthorsAsSelectItems()
        {
            var bookTypes = await Connection.GetAllAsync<AuthorEntity>();

            return bookTypes.Select(p => new AuthorSelectItemViewModel
            {
                Id = p.Id,
                Name = p.Name
            });
        }
    }
}
