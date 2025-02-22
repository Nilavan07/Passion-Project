using FootballHub.Data;
using FootballHub.Interface;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;

namespace FootballHub.Services
{
    public class MatchService : IMatch
    {
        private readonly FootballHubContext _context;

        public MatchService(FootballHubContext context)
        {
            _context = context;
        }

        // ✅ LIST MATCHES (Return Team Names)
        public async Task<IEnumerable<MatchDto>> ListMatches()
        {
            return await _context.Matches
                .Include(m => m.HomeTeam) // ✅ Include Home Team data
                .Include(m => m.AwayTeam) // ✅ Include Away Team data
                .Select(m => new MatchDto
                {
                    Id = m.Id,
                    HomeTeamName = m.HomeTeam.Name,  // ✅ Fetch Home Team Name
                    AwayTeamName = m.AwayTeam.Name,  // ✅ Fetch Away Team Name
                    HomeTeamScore = m.HomeTeamScore,
                    AwayTeamScore = m.AwayTeamScore,
                    MatchDate = m.MatchDate
                })
                .ToListAsync();
        }

        // ✅ GET SINGLE MATCH (Return Team Names)
        public async Task<MatchDto?> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.HomeTeam) // ✅ Include Home Team data
                .Include(m => m.AwayTeam) // ✅ Include Away Team data
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return null;

            return new MatchDto
            {
                Id = match.Id,
                HomeTeamName = match.HomeTeam.Name,  // ✅ Fetch Home Team Name
                AwayTeamName = match.AwayTeam.Name,  // ✅ Fetch Away Team Name
                HomeTeamScore = match.HomeTeamScore,
                AwayTeamScore = match.AwayTeamScore,
                MatchDate = match.MatchDate
            };
        }

        // ✅ CREATE NEW MATCH
        public async Task<ServiceResponse> CreateMatch(MatchCreateDto matchDto)
        {
            var match = new Models.Match
            {
                HomeTeamId = matchDto.HomeTeamId,  // ✅ Use IDs from MatchCreateDto
                AwayTeamId = matchDto.AwayTeamId,
                HomeTeamScore = matchDto.HomeTeamScore,
                AwayTeamScore = matchDto.AwayTeamScore,
                MatchDate = matchDto.MatchDate
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Match Created Successfully" };
        }

      public async Task<ServiceResponse> UpdateMatch(int id, MatchCreateDto matchDto)
{
    if (matchDto == null)
    {
        return new ServiceResponse { Success = false, Message = "Invalid request. Match data is required." };
    }

    var match = await _context.Matches.FindAsync(id);
    if (match == null)
    {
        return new ServiceResponse { Success = false, Message = "Match Not Found" };
    }

    // ✅ Update match details
    match.HomeTeamId = matchDto.HomeTeamId;
    match.AwayTeamId = matchDto.AwayTeamId;
    match.HomeTeamScore = matchDto.HomeTeamScore;
    match.AwayTeamScore = matchDto.AwayTeamScore;
    match.MatchDate = matchDto.MatchDate;

    try
    {
        _context.Matches.Update(match);
        await _context.SaveChangesAsync();
        return new ServiceResponse { Success = true, Message = "Match Updated Successfully" };
    }
    catch (Exception ex)
    {
        return new ServiceResponse { Success = false, Message = $"Error updating match: {ex.Message}" };
    }
}


        // DELETE MATCH
        public async Task<ServiceResponse> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return new ServiceResponse { Success = false, Message = "Match Not Found" };
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Match Deleted Successfully" };
        }
    }
}
