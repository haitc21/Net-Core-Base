using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DI_ASP
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<TestOption>(_configuration.GetSection("test"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/showconfig", async context =>
                {
                    // cach 1
                    // var config = context.RequestServices.GetService<IConfiguration>();
                    // var sectionTest = config.GetSection("test").Get<TestOption>();
                    // cach 2
                    var sectionTest = context.RequestServices.GetService<IOptions<TestOption>>().Value;

                    var testJson = JsonConvert.SerializeObject(sectionTest);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(testJson);
                });

            });
        }
    }
}
