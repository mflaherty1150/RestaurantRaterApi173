namespace RestaurantRaterApi.Models.Rating;

public class RatingRequest
{
    public double Score { get; set; }
    public int RestaurantId { get; set; }
}