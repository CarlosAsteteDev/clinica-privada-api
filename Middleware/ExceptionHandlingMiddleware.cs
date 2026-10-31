using CitasMedicas.Api.Entidades;

namespace CitasMedicas.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate siguiente;

    public ExceptionHandlingMiddleware(RequestDelegate siguiente)
    {
        this.siguiente = siguiente;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await siguiente(contexto);
        }
        catch (KeyNotFoundException excepcion)
        {
            await EscribirErrorAsync(contexto, StatusCodes.Status404NotFound, excepcion.Message);
        }
        catch (ArgumentException excepcion)
        {
            await EscribirErrorAsync(contexto, StatusCodes.Status400BadRequest, excepcion.Message);
        }
        catch (InvalidOperationException excepcion)
        {
            await EscribirErrorAsync(contexto, StatusCodes.Status409Conflict, excepcion.Message);
        }
        catch
        {
            await EscribirErrorAsync(contexto, StatusCodes.Status500InternalServerError, "Ocurrió un error interno en el servidor.");
        }
    }

    private static async Task EscribirErrorAsync(HttpContext contexto, int codigo, string mensaje)
    {
        contexto.Response.StatusCode = codigo;
        contexto.Response.ContentType = "application/json";
        await contexto.Response.WriteAsJsonAsync(new RespuestaError { Codigo = codigo, Mensaje = mensaje });
    }
}
