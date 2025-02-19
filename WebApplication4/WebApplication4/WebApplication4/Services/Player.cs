using FootballHub.Data;
using FootballHub.Interface;
using FootballHub.Dtos;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;

namespace FootballHub.Services
{
    public class Player : IPlayer
    {
        private readonly FootballHubContext _context;

        public Player(FootballHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayerDto>> ListPlayers()
        {
            return await _context.Players
                .Select(p => new PlayerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    TeamId = p.TeamId
                })
                .ToListAsync();
        }

        public async Task<PlayerDto> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return null;
            }

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                TeamId = player.TeamId
            };
        }

        public async Task<ServiceResponse> CreatePlayer(PlayerCreateDto playerDto)
        {
            var player = new Models.Player
            {
                Name = playerDto.Name,
                Age=playerDto.Age,
                Position=playerDto.Position,
                TeamId = playerDto.TeamId
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Player Created" };
        }

        public async Task<ServiceResponse> UpdatePlayer(int id, PlayerCreateDto playerDto)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return new ServiceResponse { Success = false, Message = "Player Not Found" };
            }

            player.Name = playerDto.Name;
            player.TeamId = playerDto.TeamId;
            player.Position = playerDto.Position;
            player.Age = playerDto.Age;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Player Updated" };
        }

        public async Task<ServiceResponse> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return new ServiceResponse { Success = false, Message = "Player Not Found" };
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Player Deleted" };
        }
    }
}
