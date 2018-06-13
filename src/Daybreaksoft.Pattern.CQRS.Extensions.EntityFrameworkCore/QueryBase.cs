using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// The base type of IQuery
    /// </summary>
    public abstract class QueryBase<TDbContext> : IQuery
    {
        protected readonly TDbContext Db;

        public QueryBase(TDbContext db)
        {
            Db = db;
        }
    }
}
