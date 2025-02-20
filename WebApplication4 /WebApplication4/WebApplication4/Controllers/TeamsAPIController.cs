using Microsoft.AspNetCore.Mvc;
using FootballHub.Interface;
using WebApplication4.Dtos;

namespace FootballHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeam _team;

        public TeamsController(ITeam team)
        {
            _team = team;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<TeamDto>> ListTeams()
        {
            return await _team.ListTeams();
        }

        [HttpGet("{id}")]
        public async Task<TeamDto> GetTeam(int id)
        {
            return await _team.GetTeam(id);
        }

        [HttpPost("Add")]
        public async Task<ServiceResponse> CreateTeam(TeamCreateDto teamDto)
        {
            return await _team.CreateTeam(teamDto);
        }

        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdateTeam(int id, TeamCreateDto teamDto)
        {
            return await _team.UpdateTeam(id, teamDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteTeam(int id)
        {
            return await _team.DeleteTeam(id);
        }
    }
}
