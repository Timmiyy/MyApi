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
            private static readonly string[] Summaries =
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            /// <summary>
           /// Generates a weather forecast for a specified city and date.
           /// </summary>
           /// <param name="request">Weather request containing city, date, and number of days.</param>
           /// <returns>A weather forecast result.</returns>
            // POST: api/weather/forecast
            [HttpPost("forecast")]
            public IActionResult GetForecast([FromBody] WeatherRequest request)
            {
                if (string.IsNullOrWhiteSpace(request.City))
                {
                    return BadRequest("City is required.");
                }
                
                var validCities = new[] { "Lagos", "Abuja", "Ibadan", "Port Harcourt" };

                if (!  validCities.Contains(request.City))
                {
                    return BadRequest("City is invalid.");
                }


                var today = DateOnly.FromDateTime(DateTime.Today);
                var maxFuture = today.AddDays(30);

                if (request.Date < maxFuture)
                {
                    return BadRequest("Date must be in the past or within 30 days into the future.");
                }
                
                var forecast = new WeatherForecast(
                    request.Date,
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                );

                return Ok(forecast);
            }
            /// <summary>
            /// Returns the current server date and time.
           /// </summary>
          /// <returns>Current date and time information.</returns>
            // GET: api/weather/date
            [HttpGet("date")]
            public IActionResult GetDate()
            {
                return Ok(new
                {
                    date = DateTime.Now,
                    message = "Current server date and time"
                });
            }
            /// <summary>
        /// Accepts and validates a weather request.
        /// </summary>
        /// <param name="request">Weather request data.</param>
        /// <returns>A confirmation message and the submitted request.</returns>
            // POST: api/weather/request
            [HttpPost("request")]
            public IActionResult CreateWeatherRequest(WeatherRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(new
                {
                    Message = "Request received successfully",
                    Data = request
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