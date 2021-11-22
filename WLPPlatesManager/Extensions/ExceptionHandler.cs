using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WLBLoggingService;
using WLBApplication.Model;
using System.Text.Json;

namespace WLBPlatesManager.Extensions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contexFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contexFeature != null)
                    {
                        logger.LogError($"We encountered an error:{contexFeature.Error}");

                        await context.Response.WriteAsync(JsonSerializer.Serialize (new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"We encountered an error:{contexFeature.Error.Message}"
                        }));
                    }
                });
            });
        }
    }
}
