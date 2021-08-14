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

namespace Mail
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
            services.AddTransient<SendMailService>();
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
                endpoints.MapGet("/sendmail", async context =>
                {
                    var mailService = context.RequestServices.GetService<SendMailService>();

                    var content = new MailContent();
                    content.To = "tranhai21121995@gmail.com";
                    content.Subject = "Test gui mail qua MailKit";
                    content.DisplayName = "Tran Hai";
                    content.Body = "<h1>Mail gui tu haitc qua MailKit</h1>";

                    string kq = await mailService.SendMailAsync(content);
                    await context.Response.WriteAsync(kq);
                });

            });
        }
    }
}
