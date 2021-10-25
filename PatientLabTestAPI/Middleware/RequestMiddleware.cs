using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMiddleware> logger;

        public RequestMiddleware(RequestDelegate next, ILogger<RequestMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            logger.LogInformation($"Path: {httpContext.Request.Path} Method: {httpContext.Request.Method} Host {httpContext.Request.Host}");
            return _next(httpContext);
        }
    }    
}
