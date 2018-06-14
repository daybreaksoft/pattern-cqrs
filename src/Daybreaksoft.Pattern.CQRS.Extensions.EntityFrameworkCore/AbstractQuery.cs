using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// The base type of IQuery
    /// </summary>
    public abstract class AbstractQuery<TDbContext> : IQuery
    {
        protected readonly TDbContext Db;

        public AbstractQuery(TDbContext db)
        {
            Db = db;
        }
    }
}
