using Irminsul.Application.Exceptions;

namespace Irminsul.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CharacterNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            
            await context.Response.WriteAsJsonAsync (new
            {
                message = ex.Message
            });

        }
        catch (CharacterAlreadyExistsException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message
            });
        }
        catch (GenericException)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                message = "Ocorreu um erro interno no servidor."
            });

        }
    }
}