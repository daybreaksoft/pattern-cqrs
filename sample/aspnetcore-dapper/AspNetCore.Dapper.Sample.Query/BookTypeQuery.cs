using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Entities;
using AspNetCore.Dapper.Sample.Query.ViewModels;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS.Extensions.Dapper;

namespace AspNetCore.Dapper.Sample.Query
{
    public class BookTypeQuery : AbstractQuery
    {
        public BookTypeQuery(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<BookTypeListItemViewModel>> GetBookTypes()
        {
            var bookTypes = await Connection.GetAllAsync<BookTypeEntity>();

            return bookTypes.Select(p => new BookTypeListItemViewModel
            {
                Id = p.Id,
                Type = p.Type
            });
        }
    }
}
