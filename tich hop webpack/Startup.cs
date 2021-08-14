using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _03.RequestResponse;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace tich_hop_webpack
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    string content = HtmlHelper.HtmlTrangchu();
                    string html = HtmlHelper.HtmlDocument("Trang chủ", menu + content);
                    await context.Response.WriteAsync(html);
                });
                endpoints.MapGet("/RequestInfo", async context =>
                {
                    string menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    string info = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");
                    string html = HtmlHelper.HtmlDocument("Thông tin của Request", menu + info);
                    await context.Response.WriteAsync(html);
                });
                endpoints.MapGet("/endcoding", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/cookies/{*action}", async context =>
                {
                    string meg = "";
                    string menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    var action = context.GetRouteValue("action") ?? "read";
                    if (action.ToString() == "write")
                    {
                        var option = new CookieOptions()
                        {
                            Path = "/",
                            Expires = DateTime.Now.AddDays(1) // thoi gian het han
                        };
                        context.Response.Cookies.Append("CookiesToi", "dasdas", option);
                        context.Response.Cookies.Append("UserName", "Haitc", option);
                        meg = "Da ghi Cookies";
                    }
                    else
                    {
                        var listcokie = context.Request.Cookies.Select((header) => $"{header.Key}: {header.Value}".HtmlTag("li"));
                        meg = string.Join("", listcokie).HtmlTag("ul");
                    }
                    string btn = "<a href=\"/Cookies/Read\" class=\"btn btn-info\">Read Cookies</a><a class=\"btn btn-success\" href=\"/Cookies/write\">Write Cookies</a>".HtmlTag("div", "container mt-4");
                    string meBox = meg.HtmlTag("div", "container mt- alert alert-danger");
                    string html = HtmlHelper.HtmlDocument("Cookies" + action.ToString(), menu + btn + meBox);
                    await context.Response.WriteAsync(html);

                });
                endpoints.MapGet("/json", async context =>
                {
                    string menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    var sp = new
                    {
                        TenSp = "Dong ho",
                        Gia = 1000,
                        NgaySX = new DateTime(2021, 8, 9)
                    };
                    string jsonRQ = JsonConvert.SerializeObject(sp);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonRQ);
                });
                endpoints.MapMethods("/Form",new string[] {"POST", "GET"}, async context =>
                {
                    string menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    string form = await RequestProcess.FormProcess(context.Request);
                    string html = HtmlHelper.HtmlDocument("Form", menu + form.HtmlTag("div", "container"));
                    await context.Response.WriteAsync(html);
                });
                
            });
        }
    }
}
