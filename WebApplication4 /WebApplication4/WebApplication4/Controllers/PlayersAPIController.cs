using Microsoft.AspNetCore.Mvc;
using FootballHub.Interface;
using WebApplication4.Dtos;

namespace FootballHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayer _player;

        public PlayersController(IPlayer player)
        {
            _player = player;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<PlayerDto>> ListPlayers()
        {
            return await _player.ListPlayers();
        }

        [HttpGet("{id}")]
        public async Task<PlayerDto> GetPlayer(int id)
        {
            return await _player.GetPlayer(id);
        }

        [HttpPost("Add")]
        public async Task<ServiceResponse> CreatePlayer(PlayerCreateDto playerDto)
        {
            return await _player.CreatePlayer(playerDto);
        }

        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdatePlayer(int id, PlayerCreateDto playerDto)
        {
            return await _player.UpdatePlayer(id, playerDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeletePlayer(int id)
        {
            return await _player.DeletePlayer(id);
        }
    }
}
