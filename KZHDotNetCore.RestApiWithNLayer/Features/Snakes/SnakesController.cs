using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KZHDotNetCore.RestApiWithNLayer.Features.Snakes
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakesController : ControllerBase
    {
        private async Task<Snake[]> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("Snakes.json");
            var snakes = JsonConvert.DeserializeObject<Snake[]>(jsonStr);
            return snakes!;
        }
        
        [HttpGet("snakes")]
        public async Task<IActionResult> Snakes()
        {
            var snakes = await GetDataAsync();
            return Ok(snakes);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSnakeById(int id)
        {
            var snakes = await GetDataAsync();
            var snake = snakes.FirstOrDefault(s => s.Id == id);
            if (snake == null)
            {
                return NotFound();
            }
            return Ok(snake);
        }

        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetSnakesByName(string name)
        {
            var snakes = await GetDataAsync();
            if (snakes is null) 
            {
                return NotFound("No data found.");
            }
            var filteredSnakes = snakes.Where(s => 
                (s.EngName?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (s.MMName?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToArray();

            if (filteredSnakes.Length == 0)
            {
                return NotFound("No data found.");
            }
            return Ok(filteredSnakes);
        }

    }
}

public class SnakesModel
{
    public Snake[] snakes { get; set; }
}

public class Snake
{
    public int Id { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}