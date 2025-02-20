using WebApplication4.Dtos;

namespace FootballHub.Interface
{
    public interface ITeam
    {
        Task<IEnumerable<TeamDto>> ListTeams();
        Task<TeamDto> GetTeam(int id);
        Task<ServiceResponse> CreateTeam(TeamCreateDto teamDto);
        Task<ServiceResponse> UpdateTeam(int id, TeamCreateDto teamDto);
        Task<ServiceResponse> DeleteTeam(int id);
    }
}
