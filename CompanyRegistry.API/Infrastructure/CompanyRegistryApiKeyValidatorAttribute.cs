using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using CompanyRegistry.API.Models;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyRegistry.API.Infrastructure
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class CompanyRegistryApiKeyValidatorAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            string apiKeyUsername = configuration.GetValue<string>(key: "ApiConfiguration:Username");
            
            string apiKeyPassword = configuration.GetValue<string>(key: "ApiConfiguration:Password");

            if (context.ActionArguments.Any(x => x.Value == null))
            {
                context.Result = new BadRequestResult();
                
                return;
            }

            StringValues headerValues;

            string username = string.Empty;

            if (context.HttpContext.Request.Headers.TryGetValue("username", out headerValues))
            {
                username = headerValues;
            }

            string password = string.Empty;

            if (context.HttpContext.Request.Headers.TryGetValue("password", out headerValues))
            {
                password = headerValues;
            }

            if (!username.Equals(apiKeyUsername) || !password.Equals(apiKeyPassword))
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            await next();
        }

    }
}
