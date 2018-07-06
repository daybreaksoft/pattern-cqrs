using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Query.ViewModels;
using Dapper;
using Daybreaksoft.Pattern.CQRS.Extensions.Dapper;

namespace AspNetCore.Dapper.Sample.Query
{
    public class BookQuery : AbstractQuery
    {
        public BookQuery(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<BookListItemViewModel>> GetBooks()
        {
            var sql = @"SELECT b.Id, b.Name, bt.Type FROM Books b JOIN BookTypes bt ON bt.Id = b.BookTypeId";

            var collection = await Connection.QueryAsync(sql);

            return collection.Select(p=>new BookListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type
            });
        }
    }
}
