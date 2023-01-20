using Perfect.Api.Common.Models;

namespace Perfect.Api.Middleware
{
    public class InternalErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public InternalErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                var envelope = new Envelope($"{errorId} - An unexpected error has occured.");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(envelope);
            }
        }
    }
}
