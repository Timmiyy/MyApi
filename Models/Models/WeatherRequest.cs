using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class WeatherRequest
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? City { get; set; }

        
        // add a property that can use Range
        [Range(1, 30)]
        public int Days { get; set; }
    }
}

