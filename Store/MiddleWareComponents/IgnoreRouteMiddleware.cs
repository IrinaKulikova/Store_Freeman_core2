using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Store.MiddleWareComponents
{
    public class IgnoreRouteMiddleware
    {
        #region private fields

        private readonly RequestDelegate _next;

        #endregion

        #region ctor

        public IgnoreRouteMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue &&
                context.Request.Path.Value.Contains("favicon.ico"))
            {

                context.Response.StatusCode = 404;

                Console.WriteLine("Ignored!");

                return;
            }

            await _next.Invoke(context);
        }
    }
}
