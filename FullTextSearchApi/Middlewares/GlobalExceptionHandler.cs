using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using FullTextSearchApi.Models.DTO;

namespace FullTextSearchApi.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            $"An error occurred while processing: {exception.Message}");
        var errorResponse = new ErrorResponse
        {
            Message = exception.Message
        };

        switch (exception)
        {
            case BadHttpRequestException:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Title = exception.GetType().Name;
                break;

            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Title = "Internal Server Error";
                break;
        }

        httpContext.Response.StatusCode = errorResponse.StatusCode;

        await httpContext
            .Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }
}