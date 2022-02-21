using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {

        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var timer = Stopwatch.StartNew();
            try
            {
                var message = "[Request]  HTTP  " + context.Request.Method + " - " + context.Request.Path;
                Console.WriteLine(message);
                await _next.Invoke(context);
                timer.Stop();

                message = "[Request]  HTTP  " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + timer.Elapsed.TotalMilliseconds;
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
              
                timer.Stop();
                await HandleExceptionAsync(context,ex,timer);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch timer)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]  HTTP "+ context.Request.Method + " - " + context.Response.StatusCode + " Error Message : "+ex.Message + "in"+ timer.Elapsed.TotalMilliseconds;
            var result = JsonConvert.SerializeObject(new {error= ex.Message, statusCode= context.Response.StatusCode}, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
}