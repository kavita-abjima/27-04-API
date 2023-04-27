using System.ComponentModel.DataAnnotations;

namespace CityInfo3.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

    }
}
