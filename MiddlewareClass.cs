using System.Diagnostics;

namespace API
{
    public class MiddlewareClass:IMiddleware
    {
        private readonly ILogger _logger;
        
        public MiddlewareClass(ILogger logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           
                Stopwatch timer = new Stopwatch();
                timer.Start();
                await next.Invoke(context);
                timer.Stop();
                TimeSpan ts = timer.Elapsed;
                if (ts.Seconds > 4)
                {
                    throw new Exception();
                }
            }
            
        }
    }
}
