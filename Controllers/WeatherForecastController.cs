using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("task")]
        public ActionResult<IEnumerable<WeatherForecast>> TaskResults([FromQuery] int count, [FromBody] Temp temp)
        {
            if (count < 0 || temp.max < temp.min)
            {
                return BadRequest();
            }

            var result = _service.Get(count, temp.min, temp.max);
            return Ok(result);
        }
    }
}
