using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballHub1.Data;
using FootballHub1.Models;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly FootballHub1Context _context;

    public PlayersController(FootballHub1Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
    {
        return await _context.Players.Include(p => p.Team).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Player>> PostPlayer(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlayers), new { id = player.PlayerId }, player);
    }
}
