using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    /// <summary>
/// Represents weather data.
/// </summary>
    public class WeatherRequest
    {
        /// <summary>
        /// Requested forecast date.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        /// <summary>
        /// City for the forecast.
        /// </summary>
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? City { get; set; }

        /// <summary>
        /// Number of forecast days requested.
        /// </summary>
        
        // add a property that can use Range
        [Range(1, 30)]
        public int Days { get; set; }
    }
}

