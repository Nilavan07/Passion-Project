using WebApplication4.Dtos;

namespace FootballHub.Interface
{
    public interface IMatch
    {
        Task<IEnumerable<MatchDto>> ListMatches();
        Task<MatchDto?> GetMatch(int id);
        Task<ServiceResponse> CreateMatch(MatchCreateDto matchDto);
        Task<ServiceResponse> UpdateMatch(int id, MatchCreateDto matchDto);
        Task<ServiceResponse> DeleteMatch(int id);
    }
}
