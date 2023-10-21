using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterApi.Models.Restaurant;

public class RestaurantCreate
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Location { get; set; } = string.Empty;
}