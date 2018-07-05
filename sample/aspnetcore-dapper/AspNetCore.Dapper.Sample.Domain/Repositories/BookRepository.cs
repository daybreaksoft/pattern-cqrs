using System.Data;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Entities;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Domain.Repositories
{
    public class BookRepository : IRepository<BookAggregate>
    {
        protected readonly IDbConnection Connection;

        public BookRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        public Task<BookAggregate> FindAsync(object id, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertAsync(BookAggregate aggregate, IDbTransaction transaction = null)
        {
            // Insert book
            var newBook = new BookEntity
            {
                Name = aggregate.Name,
                BookTypeId = aggregate.BookTypeId
            };

            await Connection.InsertAsync(newBook);

            // Insert book author id
            if (aggregate.AuthorIds != null)
            {
                foreach (var authorId in aggregate.AuthorIds)
                {
                    await Connection.InsertAsync(new BookAuthorEntity
                    {
                        BookId = newBook.Id,
                        AuthorId = authorId
                    });
                }
            }
        }

        public Task UpdateAsync(BookAggregate aggreagate, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(object id, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }
    }
}