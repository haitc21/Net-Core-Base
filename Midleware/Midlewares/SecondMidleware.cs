using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Midlewares
{
    public static class UseSecondMidlewareMethod
    {
        public static void UseSecondMidleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SeconMidleware>();
        }
    }
    /// <summary>
    /// dung kieu nay thi fai dang ki DI
    /// </summary>
    public class SeconMidleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string path = context.Request.Path;
            if (path == "xxx")
            {
                // luon fai viet header truoc write (neu co header)
                context.Response.Headers.Add("SeconMidleware", "Khong duoc truy cap");
                await context.Response.WriteAsync("Ban khong duoc tuy cap");
            }
            else
            {
                context.Response.Headers.Add("SeconMidleware", "Duoc phep truy cap");
                var dataFirst = context.Items["dataFirst"];
                Console.WriteLine("dataFirs " + (string)dataFirst);
                if (dataFirst != null)
                {
                    await context.Response.WriteAsync((string)dataFirst);
                }
                await next(context);
            }
        }
    }
}