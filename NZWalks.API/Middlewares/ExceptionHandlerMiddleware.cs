using System.Net;

namespace NZWalks.API.Middlewares
{
    // Este metodo esta para capturar cualquier error y agregarlo a los Logs
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Agrega esta excepcion a los Logs
                logger.LogError(ex, $"{errorId}: {ex.Message}");

                // Regresa una respuesta Custom Error
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Algo salio mal! Estamos buscando como resolverlo. "
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
