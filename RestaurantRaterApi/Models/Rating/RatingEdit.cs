using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterApi.Models.Rating;

public class RatingEdit
{
    [Required]
    [Range(0,5)]
    public double Score { get; set; }

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
}