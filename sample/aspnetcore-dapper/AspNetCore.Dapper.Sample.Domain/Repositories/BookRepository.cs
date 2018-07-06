using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Data.Entities;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Dapper;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Domain.Repositories
{
    public class BookRepository : IRepository<BookAggregate>
    {
        protected readonly IDbConnection Connection;
        protected readonly IAggregateBus AggregateBus;

        public BookRepository(IDbConnection connection, IAggregateBus aggregateBus)
        {
            Connection = connection;
            AggregateBus = aggregateBus;
        }

        public async Task<BookAggregate> FindAsync(object id, IDbTransaction transaction = null)
        {
            var sql = "SELECT * FROM Books b  JOIN BookAuthors ba ON ba.BookId = b.Id WHERE b.Id = @Id";

            BookAggregate book = null;

            await Connection.QueryAsync<BookEntity, BookAuthorEntity, BookEntity>(sql,
                (bookEntity, bookAuthorEntity) =>
                {
                    if (book == null)
                    {
                        book = AggregateBus.BuildAggregate<BookAggregate>(bookEntity.Id);
                        book.Name = bookEntity.Name;
                        book.BookTypeId = bookEntity.BookTypeId;
                        book.AuthorIds = new List<int>();
                    }

                    if (book.AuthorIds.All(p => p != bookAuthorEntity.AuthorId))
                    {
                        book.AuthorIds.Add(bookAuthorEntity.AuthorId);
                    }

                    return bookEntity;

                }, new {Id = id});

            return book;
        }

        public async Task InsertAsync(BookAggregate aggregate, IDbTransaction transaction = null)
        {
            // Insert book
            var newBook = new BookEntity
            {
                Name = aggregate.Name,
                BookTypeId = aggregate.BookTypeId
            };

            await Connection.InsertAsync(newBook, transaction);

            // Insert book author id
            if (aggregate.AuthorIds != null)
            {
                foreach (var authorId in aggregate.AuthorIds)
                {
                    await Connection.InsertAsync(new BookAuthorEntity
                    {
                        BookId = newBook.Id,
                        AuthorId = authorId
                    }, transaction);
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