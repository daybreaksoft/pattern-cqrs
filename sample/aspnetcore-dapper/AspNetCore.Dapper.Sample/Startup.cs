using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Command.BookType;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using AspNetCore.Sample.Query;
using Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Dapper.Sample
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
            services.AddCQRSWithDapper(builder =>
            {
                var domainAssembly = typeof(BookTypeAggregate).GetTypeInfo().Assembly;

                builder.ForDbConnection(s =>
                    {
                        s.AddScoped<IDbConnection>(sp => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
                    });
                builder.ForCommandExecutor(typeof(CreateBookTypeCommand).GetTypeInfo().Assembly);
                builder.ForRepository(domainAssembly, "AspNetCore.Dapper.Sample.Domain.Repositories");
                builder.ForAggregate(domainAssembly, "AspNetCore.Dapper.Sample.Domain.Aggregates");
                builder.ForEventHandler(domainAssembly, "AspNetCore.Dapper.Sample.Domain.Events");
                builder.ForPostCommitEventHandler(domainAssembly, "AspNetCore.Dapper.Sample.Domain.Events");
                builder.ForQuery(typeof(BookTypeQuery).GetTypeInfo().Assembly);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
