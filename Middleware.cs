

namespace API
{
    public class Middleware : IMiddleware
    {
        private readonly ILogger<Middleware> _logger;
        public Middleware(ILogger<Middleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) 
        {
            try
            {
                await next.Invoke(context);
            } 
            catch (BadHttpRequestException badreq )
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("bad");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("sth went wrong");
            }
        }
    }
}
