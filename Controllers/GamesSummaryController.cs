using Microsoft.AspNetCore.Mvc;
using NetCoreCrudMongo.Models;
using NetCoreCrudMongo.Services;

namespace NetCoreCrudMongo.Controllers;

[Controller]
[Route("api/[controller]")]
public class GamesSummaryController : Controller
{
    private readonly MongoDBService _mongoDbService;

    public GamesSummaryController(MongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    public async Task<List<GamesSummary>> Get()
    {
        return await _mongoDbService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GamesSummary gamesSummary)
    {
        await _mongoDbService.CreateAsync(gamesSummary);
        return CreatedAtAction(nameof(Get), new { id = gamesSummary.Id }, gamesSummary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> addGame(string id, [FromBody] Game game)
    {
        await _mongoDbService.AddGameToSummary(id, game);
        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDbService.DeleteSummary(id);
        return NoContent();
    }
}