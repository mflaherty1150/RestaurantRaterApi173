using Microsoft.AspNetCore.Mvc;
using RestaurantRaterApi.Data;
using RestaurantRaterApi.Models.Rating;

namespace RestaurantRaterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
    private readonly ILogger<RatingController> _logger;
    private readonly RestaurantDbContext _context;

    public RatingController(ILogger<RatingController> logger, RestaurantDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RateRestaurant([FromForm] RatingEdit model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Rating newRating = new()
        {
            Score = model.Score,
            RestaurantId = model.RestaurantId
        };

        _context.Ratings.Add(newRating);
        await _context.SaveChangesAsync();

        return Ok();
    }
}