using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ExceptionHandler
    {
        readonly RequestDelegate requestDelegate;

        public ExceptionHandler(RequestDelegate requestDelegate, IWebHostEnvironment environment)
        {
            this.requestDelegate = requestDelegate;
            Environment = environment;

        }
        public IWebHostEnvironment Environment { get; }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);

            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = 500;
                string message;
                if (e.InnerException == null)
                    message = e.Message;
                else
                    message = e.InnerException.Message;
                if (Environment.IsDevelopment())
                {
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new {message = e.StackTrace }));

                }
                else
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { message }));

            }
        }
    }
}
