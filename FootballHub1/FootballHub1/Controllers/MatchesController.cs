using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballHub1.Data;
using FootballHub1.Models;

[Route("api/[controller]")]
[ApiController]
public class MatchesController : ControllerBase
{
    private readonly FootballHub1Context _context;

    public MatchesController(FootballHub1Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
    {
        return await _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Match>> PostMatch(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMatches), new { id = match.MatchId }, match);
    }
}
