using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Builder;

namespace Midlewares
{
    public static class UseFirstMidlewareMethod
    {
        public static void UseFirstMidleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FirstMidleware>();
        }
    }
    public class FirstMidleware
    {
        private readonly RequestDelegate _next;
        // Cai nay de truyen vao cac midleware tiep theo
        // Neu khong co thi no se thanh Teminate Midlewar
        public FirstMidleware(RequestDelegate next)
        {
            _next = next;
        }

        // cai nay goi khi httpcontxt di qua pipeline
        public async Task InvokeAsync(HttpContext context)
        {
            context.Items.Add("dataFirst", $"<h1>URL: {context.Request.Path}</h1>");
          // vi Secondmidddleware dat phia sau co set header nen khong write o first duoc
          //  await context.Response.WriteAsync($"<h1>URL: {context.Request.Path}</h1>");
            await _next(context);
        }
    }
}