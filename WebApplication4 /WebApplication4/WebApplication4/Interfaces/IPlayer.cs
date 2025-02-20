using WebApplication4.Dtos;

namespace FootballHub.Interface
{
    public interface IPlayer
    {
        Task<IEnumerable<PlayerDto>> ListPlayers();
        Task<PlayerDto> GetPlayer(int id);
        Task<ServiceResponse> CreatePlayer(PlayerCreateDto playerDto);
        Task<ServiceResponse> UpdatePlayer(int id, PlayerCreateDto playerDto);
        Task<ServiceResponse> DeletePlayer(int id);
    }
}
