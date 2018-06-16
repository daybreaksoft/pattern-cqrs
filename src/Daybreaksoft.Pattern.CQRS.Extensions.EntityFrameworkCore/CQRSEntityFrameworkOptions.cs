using Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// CQRS options of entity frmework builder
    /// </summary>
    public class CQRSEntityFrameworkOptions : CQRSOptions
    {
        /// <summary>
        /// The type of implemented class of IDbContext
        /// </summary>
        public Type DbContextType { get; protected set; }

        /// <summary>
        /// Action that add database context implemented class
        /// </summary>
        public Action<IServiceCollection> AddDbContextAction { get; protected set; }

        /// <summary>
        /// Set the type of implemented class of IDbContext
        /// </summary>
        public void ForDbContext<TDbContext>() where TDbContext : DbContext
        {
            DbContextType = typeof(TDbContext);

            AddDbContextAction = null;
        }

        /// <summary>
        /// Set custom DI action for database context
        /// </summary>
        public void ForDbContext(Action<IServiceCollection> action)
        {
            AddDbContextAction = action;

            DbContextType = null;
        }
    }
}
