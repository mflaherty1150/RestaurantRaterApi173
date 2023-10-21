using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterApi.Data;

public class Restaurant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Location { get; set; } = string.Empty;
}