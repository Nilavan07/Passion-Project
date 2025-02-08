using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballHub1.Data;
using FootballHub1.Models;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly FootballHub1Context _context;

    public TeamsController(FootballHub1Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        return await _context.Teams.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Team>> PostTeam(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTeams), new { id = team.TeamId }, team);
    }
}
