    using Microsoft.AspNetCore.Mvc;
    using MyApi.Models;

    namespace MyApi.Controllers
    {
        /// <summary>
        /// Handles weather-related operations.
        /// </summary>
        [ApiController]
        [Route("api/[controller]")]
        public class WeatherController : ControllerBase
        {
            private readonly ILogger<WeatherController> _logger;
            private static readonly string[] Summaries =
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }
            /// <summary>
           /// Generates a weather forecast for a specified city and date.
           /// </summary>
           /// <param name="request">Weather request containing city, date, and number of days.</param>
           /// <returns>A weather forecast result.</returns>
            // POST: api/weather/forecast
     [HttpPost("forecast")]
public IActionResult GetForecast([FromBody] WeatherRequest request)
{
   _logger.LogInformation(
    "Forecast generated successfully for {City}. Correlation ID: {CorrelationId}",
    request.City,
    HttpContext.Items["CorrelationId"]
);
    //throw new Exception("Exception outside try-catch");
    try
    {
        _logger.LogInformation(
        "Forecast request received for city: {City}. Correlation ID: {CorrelationId}",
        request.City,
        HttpContext.Items["CorrelationId"]
    );

      // throw new Exception("Exception inside try-catch");
        if (string.IsNullOrWhiteSpace(request.City))
        {
            _logger.LogWarning("City was missing in request");

            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "City is required."
            });
        }

        var validCities = new[] { "Lagos", "Abuja", "Ibadan", "Port Harcourt" };

        if (!validCities.Contains(request.City))
        {
            _logger.LogWarning("Invalid city attempted: {City}", request.City);

            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "City is invalid."
            });
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var maxFuture = today.AddDays(30);

        if (request.Date > maxFuture)
        {
            _logger.LogWarning("Future date too far: {Date}", request.Date);

            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "Date cannot be more than 30 days in the future."
            });
        }

        var forecast = new WeatherForecast(
            request.Date,
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)]
        );

        _logger.LogInformation("Forecast generated successfully for {City}", request.City);

        return Ok(new ApiResponse<WeatherForecast>
        {
            Success = true,
            Message = "Forecast generated successfully.",
            Data = forecast
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(
    ex,
    "Error occurred in GetForecast. Correlation ID: {CorrelationId}",
    HttpContext.Items["CorrelationId"]
);

        return StatusCode(500, new ApiResponse<object>
        {
            Success = false,
            Message = "An unexpected error occurred."
        });
    }
}
            /// <summary>
            /// Returns the current server date and time.
           /// </summary>
          /// <returns>Current date and time information.</returns>
            // GET: api/weather/date
          [HttpGet("date")]
public IActionResult GetDate([FromQuery] string name = "Guest")
{
    try
    {
        _logger.LogInformation(
            "Date endpoint called by {Name}",
            name
        );

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = $"Hello {name}",
            Data = new
            {
                Date = DateTime.Now
            }
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred in GetDate");

        return StatusCode(500, new ApiResponse<object>
        {
            Success = false,
            Message = "An unexpected error occurred."
        });
    }
}
            

        /// <summary>
    /// Represents a weather forecast response.
    /// </summary>
        public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
        {
            /// <summary>
        /// Temperature converted to Fahrenheit.
        /// </summary>
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
 }