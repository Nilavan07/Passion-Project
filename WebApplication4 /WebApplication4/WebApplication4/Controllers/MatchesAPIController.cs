using Microsoft.AspNetCore.Mvc;
using FootballHub.Interface;
using WebApplication4.Dtos;

namespace FootballHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatch _match;

        public MatchesController(IMatch match)
        {
            _match = match;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<MatchDto>> ListMatches()
        {
            return await _match.ListMatches();
        }

        [HttpGet("{id}")]
        public async Task<MatchDto> GetMatch(int id)
        {
            return await _match.GetMatch(id);
        }

        [HttpPost("Add")]
        public async Task<ServiceResponse> CreateMatch(MatchCreateDto matchDto)
        {
            return await _match.CreateMatch(matchDto);
        }

        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdateMatch(int id, MatchCreateDto matchDto)
        {
            return await _match.UpdateMatch(id, matchDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteMatch(int id)
        {
            return await _match.DeleteMatch(id);
        }
    }
}
