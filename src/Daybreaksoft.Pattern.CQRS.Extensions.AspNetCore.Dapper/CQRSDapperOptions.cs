using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore.Dapper
{
    /// <summary>
    /// CQRS options of dapper builder
    /// </summary>
    public class CQRSDapperOptions : CQRSOptions
    {
        public string ConnectionString { get; set; }

        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// Set the connection string of IDbConnection
        /// </summary>
        public void ForDbConnection(string connectionString, DatabaseType type = DatabaseType.SqlServer)
        {
            ConnectionString = connectionString;
            DatabaseType = type;
        }

        /// <summary>
        /// Set the connection string of IDbConnection
        /// </summary>
        public void ForDbConnection(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IDbConnection).Name, action);
        }
    }
}
