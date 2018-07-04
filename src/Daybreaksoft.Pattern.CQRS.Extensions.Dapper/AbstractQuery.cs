using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Daybreaksoft.Pattern.CQRS.Extensions.Dapper
{
    /// <summary>
    /// The base type of IQuery
    /// </summary>
    public abstract class AbstractQuery : IQuery
    {
        protected readonly IDbConnection Connection;

        protected AbstractQuery(IDbConnection connection)
        {
            Connection = connection;
        }
    }
}
