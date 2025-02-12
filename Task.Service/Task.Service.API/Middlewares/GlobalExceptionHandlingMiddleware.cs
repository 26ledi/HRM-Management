using HRManagement.Exceptions.Shared;
using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;

namespace Task.Service.API.Middlewares
{
    public static class GlobalExceptionHandlingMiddleware
    {
        public static void UseExceptionMiddleware(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var (statusCode, message) = contextFeature?.Error switch
                    {
                        NotFoundException => (StatusCodes.Status404NotFound, contextFeature.Error.Message),
                        AlreadyExistsException => (StatusCodes.Status400BadRequest, contextFeature.Error.Message),

                        _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                    };

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = statusCode,
                        Message = message
                    });
                });
            });
        }
    }
}
