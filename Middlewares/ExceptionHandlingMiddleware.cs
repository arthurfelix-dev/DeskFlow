using System.Text.Json;

namespace DeskFlow.API.Middlewares;

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
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var resposta = new
            {
                mensagem = "Ocorreu um erro interno no servidor."
            };

            var json = JsonSerializer.Serialize(resposta);

            await context.Response.WriteAsync(json);
        }
    }
}