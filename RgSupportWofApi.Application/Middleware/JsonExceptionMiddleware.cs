using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RgSupportWofApi.Application.Middleware
{
    public class JsonExceptionMiddleware
    {
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var ex = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var error = new
            {
                error = ex.Message
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            using (var writer = new StreamWriter(httpContext.Response.Body))
            {
                new JsonSerializer().Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JsonExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsonExceptionMiddleware>();
        }
    }
}
