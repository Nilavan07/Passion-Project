using FootballHub.Data;
using FootballHub.Interface;
using FootballHub.Dtos;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;

namespace FootballHub.Services
{
    public class Team : ITeam
    {
        private readonly FootballHubContext _context;

        public Team(FootballHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamDto>> ListTeams()
        {
            return await _context.Teams
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Coach = t.Coach,
                    Stadium = t.Stadium,
                    FoundedYear = t.FoundedYear
                })
                .ToListAsync();
        }

        public async Task<TeamDto> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return null;
            }

            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Coach = team.Coach,
                Stadium = team.Stadium,
                FoundedYear = team.FoundedYear
            };
        }

        public async Task<ServiceResponse> CreateTeam(TeamCreateDto teamDto)
        {
            var team = new Models.Team
            {
                Name = teamDto.Name,
                Coach = teamDto.Coach,
                Stadium = teamDto.Stadium,
                FoundedYear = teamDto.FoundedYear
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Team Created" };
        }

        public async Task<ServiceResponse> UpdateTeam(int id, TeamCreateDto teamDto)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return new ServiceResponse { Success = false, Message = "Team Not Found" };
            }

            team.Name = teamDto.Name;
            team.Coach = teamDto.Coach;
            team.Stadium = teamDto.Stadium;
            team.FoundedYear = teamDto.FoundedYear;

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Team Updated" };
        }

        public async Task<ServiceResponse> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return new ServiceResponse { Success = false, Message = "Team Not Found" };
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Team Deleted" };
        }
    }
}
