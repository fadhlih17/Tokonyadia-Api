using System.Net;
using Tokonyadia_Api.DTO;

namespace Tokonyadia_Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
        }
        catch (Exception e) // custom exception
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // apapun response nya akan menampilkan json
        context.Response.ContentType = "application/json";
        // instance error response
        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = exception.Message;
                break;
            case not null:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal Server Error";
                break;
        }

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}