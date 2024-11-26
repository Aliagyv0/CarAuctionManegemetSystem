using CarAuctionApi.Data.Exceptions;
using CarAuctionApi.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace CarAuctionApi.App.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static void HandleException(this WebApplication app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    string message = "Tehnical error occured.";
                    int statusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var exception = contextFeature?.Error;

                    if (exception is EntityNotFoundException)
                        statusCode = (int)HttpStatusCode.NotFound;

                    else if (exception is DeleteMainImageException)
                        statusCode = (int)HttpStatusCode.Conflict;

                    else if (exception is UserNotFoundException)
                        statusCode = (int)HttpStatusCode.NotFound;

                    else if (exception is TokenExpiredException)
                        statusCode = (int)HttpStatusCode.Gone;


                    message = exception?.Message;
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        new { error = message }));
                });
            });
        }
    }
}