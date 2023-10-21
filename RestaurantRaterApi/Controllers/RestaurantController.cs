using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterApi.Data;
using RestaurantRaterApi.Models.Restaurant;

namespace RestaurantRaterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public RestaurantController(RestaurantDbContext context)
    {
        _context = context;
    }

    // Async GET Endpoint
    [HttpGet]
    public async Task<IActionResult> GetRestaurantsAsync()
    {
        List<Restaurant> restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
        List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                AverageScore = r.AverageRating,
            }).ToList();

        return Ok(restaurantList);
    }

    [HttpGet("id:int")] // https://localhost7129/Restaurant/1
    public async Task<IActionResult> GetRestaurantByIdAsync(int id)
    {
        Restaurant? restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);

        if (restaurant is null) 
            return NotFound();
        
        return Ok(restaurant);
    }

    // Async POST Endpoint
    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromBody] RestaurantCreate request) // FromBody=JSON
    {
        if (ModelState.IsValid)
        {
            Restaurant restaurantToAdd = new()
            {
                Name = request.Name,
                Location = request.Location
            };

            _context.Restaurants.Add(restaurantToAdd);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
    {
        var oldRestaurant = await _context.Restaurants.FindAsync(id);

        if (oldRestaurant == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        if (!string.IsNullOrEmpty(model.Name))
            oldRestaurant.Name = model.Name;

        if (!string.IsNullOrEmpty(model.Location))
            oldRestaurant.Location = model.Location;

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
            return NotFound();
        
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
        return Ok();
    }
}