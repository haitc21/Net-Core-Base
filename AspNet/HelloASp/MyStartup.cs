using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloASp
{
    public class MyStartup
    {
        // Cai nay de dang ki DI
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Cau tao len 1 pipeline boi cac midleware
        /// xu ly cac HttpRequest tra ve cac HttpRespone
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // khi reuest den thi no kiem tra xem trong folder wwwroot  co file k neu co thi no se dong pipeline luon
            // Muon no k vao wwwroot thi sua webBuilder trong ham Main
            app.UseStaticFiles();

            app.UseRouting(); // DIEU HUONG DEN CAC EndPoint

            // Chi ra duong dan nao tra ve cai gi
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Day la My startup!");
                });
                endpoints.MapGet("/abc", async context =>
                {
                    await context.Response.WriteAsync("Day la abc!");
                });
                endpoints.MapGet("/Home", async context =>
                {
                    await context.Response.WriteAsync("<h1>Day la Home!</h1>");
                });
                string html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Trang web đầu tiên</title>
                    <link rel=""stylesheet"" href=""/css/bootstrap.min.css"" />
                    <script src=""/js/jquery.min.js""></script>
                    <script src=""/js/popper.min.js""></script>
                    <script src=""/js/bootstrap.min.js""></script>


                </head>
                <body>
                    <nav class=""navbar navbar-expand-lg navbar-dark bg-danger"">
                            <a class=""navbar-brand"" href=""#"">Brand-Logo</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#my-nav-bar"" aria-controls=""my-nav-bar"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                    <span class=""navbar-toggler-icon""></span>
                            </button>
                            <div class=""collapse navbar-collapse"" id=""my-nav-bar"">
                            <ul class=""navbar-nav"">
                                <li class=""nav-item active"">
                                    <a class=""nav-link"" href=""#"">Trang chủ</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link"" href=""#"">Học HTML</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link disabled"" href=""#"">Gửi bài</a>
                                </li>
                        </ul>
                        </div>
                    </nav> 
                    <p class=""display-4 text-danger"">Đây là trang đã có Bootstrap</p>
                </body>
                </html>
    ";
                endpoints.MapGet("/test", async context =>
            {
                await context.Response.WriteAsync(html);
            });
            });

            // Cai nay la midleware cuoi cunge
            // Neu ma cai duong dan no khong ton tai thi no se di vao day
            // app.Run(async (HttpContext context) =>
            // {
            //     await context.Response.WriteAsync("Day la Run trong MyStratup");
            // });
          
            app.UseStatusCodePages(); // dat o dau no cung xu ly cuoi
        }
    }
}
