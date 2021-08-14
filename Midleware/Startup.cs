using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Midlewares;

namespace Midleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SeconMidleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // app.UseMiddleware<FirstMidleware>();
            app.UseFirstMidleware();
            app.UseSecondMidleware();
            //app.UseMiddleware<SeconMidleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Trang chu");
                });
            });

            // re nhanh
            app.Map("/admin", (app1) =>
            {
                // khai bao cac middleware khi di vao admin
                app1.UseRouting();

                app1.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/role", async context =>
                    {
                        await context.Response.WriteAsync("Quan li quyen");
                    });
                });
                app1.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Hello ADMIN");
                });
            });
            app.Map("/user", (app1) =>
{
    // khai bao cac middleware khi di vao admin
    app1.Run(async (context) =>
{
await context.Response.WriteAsync("Hello User");
});
});

            // terminate
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello .net");
            });
        }
    }
}
