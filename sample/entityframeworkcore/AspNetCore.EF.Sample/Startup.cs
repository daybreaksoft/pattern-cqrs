using System.Reflection;
using AspNetCore.EF.Sample.Command.User;
using AspNetCore.EF.Sample.Core;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Data;
using AspNetCore.EF.Sample.Query.User;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.EF.Sample
{
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

            services.AddCQRSWithEntityFramework(builder =>
            {
                var domainAssembly = typeof(UserModel).GetTypeInfo().Assembly;

                builder.ForDbContext<SampleDbContext>();
                builder.ForCommandExecutor(typeof(CreateUserCommandExecutor).GetTypeInfo().Assembly);
                builder.ForRepository(domainAssembly);
                builder.ForDomainAppService(domainAssembly);
                builder.ForEventHandler(domainAssembly);
                builder.ForPostCommitEventHandler(domainAssembly);
                builder.ForQuery(typeof(UserQuery).GetTypeInfo().Assembly);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
