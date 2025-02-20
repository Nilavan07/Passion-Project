using FootballHub.Data;
using FootballHub.Interface;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;

namespace FootballHub.Services
{
    public class PlayerService : IPlayer
    {
        private readonly FootballHubContext _context;

        public PlayerService(FootballHubContext context)
        {
            _context = context;
        }

        // ✅ LIST ALL PLAYERS (WITH TEAM NAME)
        public async Task<IEnumerable<PlayerDto>> ListPlayers()
        {
            return await _context.Players
                .Include(p => p.Team) // Fetch Team data
                .Select(p => new PlayerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Position = p.Position,
                    Age = p.Age,
                    GoalsScored = p.GoalsScored,
                    Assists = p.Assists,
                    TeamId = p.TeamId,
                    TeamName = p.Team != null ? p.Team.Name : "Unknown"
                })
                .ToListAsync();
        }

        // ✅ GET A SINGLE PLAYER BY ID
        public async Task<PlayerDto> GetPlayer(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
                return null;

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                GoalsScored = player.GoalsScored,
                Assists = player.Assists,
                TeamId = player.TeamId,
                TeamName = player.Team != null ? player.Team.Name : "Unknown"
            };
        }

        // ✅ CREATE A NEW PLAYER
        public async Task<ServiceResponse> CreatePlayer(PlayerCreateDto playerDto)
        {
            var player = new Models.Player
            {
                Name = playerDto.Name,
                Position = playerDto.Position,
                Age = playerDto.Age,
                GoalsScored = playerDto.GoalsScored,
                Assists = playerDto.Assists,
                TeamId = playerDto.TeamId
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Player Created" };
        }

        // ✅ UPDATE EXISTING PLAYER
        public async Task<ServiceResponse> UpdatePlayer(int id, PlayerCreateDto playerDto)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return new ServiceResponse { Success = false, Message = "Player Not Found" };
            }

            player.Name = playerDto.Name;
            player.Position = playerDto.Position;
            player.Age = playerDto.Age;
            player.GoalsScored = playerDto.GoalsScored;
            player.Assists = playerDto.Assists;
            player.TeamId = playerDto.TeamId;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Player Updated" };
        }

        // ✅ DELETE PLAYER
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
