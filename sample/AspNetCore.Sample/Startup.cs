using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCore.Sample.Commands;
using AspNetCore.Sample.Commands.User;
using AspNetCore.Sample.Repository;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Sample
{
    public class DefaultDependencyInjection : IDependencyInjection
    {
        protected readonly IServiceProvider ServiceProvider;

        public DefaultDependencyInjection(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public TService GetService<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.TryAddScoped<IDbContext, SampleDbContext>();
            services.TryAddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
            services.TryAddScoped<IDependencyInjection, DefaultDependencyInjection>();
            services.TryAddScoped<ICommandBus, DefaultCommandBus>();

            var assembly = typeof(CreateUserCommandExecutor).GetTypeInfo().Assembly;
            var targetInterfaceName = typeof(ICommandExecutor<>).Name;
            foreach (var implementationType in assembly.GetExportedTypes())
            {
                var targetInterface = implementationType.GetInterfaces().SingleOrDefault(p => p.Name == targetInterfaceName);

                if (targetInterface != null)
                {
                    services.AddTransient(targetInterface, implementationType);
                }
            }
            //services.TryAddTransient<ICommandExecutor<SubmitUserCommand>, CreateUserCommandExecutor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Verifies whether database exists
            if (env.IsDevelopment())
            {
                EnsureDatabaseExists();
            }
        }

        /// <summary>
        /// Verifies whether database exists
        /// Create new database if not exists
        /// </summary>
        private void EnsureDatabaseExists()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            using (var sampleDbContext = new SampleDbContext(builder.Options))
            {

                sampleDbContext.Database.EnsureCreated();
            }
        }
    }
}
