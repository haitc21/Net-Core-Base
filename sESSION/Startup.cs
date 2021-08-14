using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace sESSION
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDistributedMemoryCache(); // luu session trong Memory (RAm)
            services.AddDistributedSqlServerCache(opt =>
            {
                opt.ConnectionString = "Data Source=localhost,1433;Initial Catalog=AppDb;User ID=SA;Password=Password123";
                opt.SchemaName = "dbo";
                opt.TableName = "Session";
            });
            services.AddSession(opt =>
            {
                opt.Cookie.Name = "TranHai";
                opt.IdleTimeout = new TimeSpan(0, 1, 0);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    int? contAcces;
                    contAcces = context.Session.GetInt32("count");
                    if (contAcces == null)
                        contAcces = 0;
                    else
                        contAcces += 1;
                    context.Session.SetInt32("count", contAcces.Value);

                    await context.Response.WriteAsync($"So lan truy cap: {contAcces.Value.ToString()} ");
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/session", async context =>
                {
                    int? contAcces;
                    contAcces = context.Session.GetInt32("count");
                    if (contAcces == null)
                        contAcces = 0;
                    else
                        contAcces += 1;
                    context.Session.SetInt32("count", contAcces.Value);

                    await context.Response.WriteAsync($"So lan truy cap: {contAcces.Value.ToString()} ");
                });
            });
        }
    }
}
