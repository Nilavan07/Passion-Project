using FootballHub.Data;
using FootballHub.Interface;
using FootballHub.Dtos;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;

namespace FootballHub.Services
{
    public class Match : IMatch
    {
        private readonly FootballHubContext _context;

        public Match(FootballHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatchDto>> ListMatches()
        {
            return await _context.Matches
                .Select(m => new MatchDto
                {
                    Id = m.Id,
                    HomeTeamId = m.HomeTeamId,
                    AwayTeamId = m.AwayTeamId,
                    HomeTeamScore = m.HomeTeamScore,
                    AwayTeamScore = m.AwayTeamScore,
                    MatchDate = m.MatchDate
                })
                .ToListAsync();
        }

        public async Task<MatchDto> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return null;
            }

            return new MatchDto
            {
                Id = match.Id,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                HomeTeamScore = match.HomeTeamScore,
                AwayTeamScore = match.AwayTeamScore,
                MatchDate = match.MatchDate
            };
        }

        public async Task<ServiceResponse> CreateMatch(MatchCreateDto matchDto)
        {
            var match = new Models.Match
            {
                HomeTeamId = matchDto.HomeTeamId,
                AwayTeamId = matchDto.AwayTeamId,
                HomeTeamScore = matchDto.HomeTeamScore,
                AwayTeamScore = matchDto.AwayTeamScore,
                MatchDate = matchDto.MatchDate
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Match Created" };
        }

        public async Task<ServiceResponse> UpdateMatch(int id, MatchCreateDto matchDto)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return new ServiceResponse { Success = false, Message = "Match Not Found" };
            }

            match.HomeTeamId = matchDto.HomeTeamId;
            match.AwayTeamId = matchDto.AwayTeamId;
            match.HomeTeamScore = matchDto.HomeTeamScore;
            match.AwayTeamScore = matchDto.AwayTeamScore;
            match.MatchDate = matchDto.MatchDate;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Match Updated" };
        }

        public async Task<ServiceResponse> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return new ServiceResponse { Success = false, Message = "Match Not Found" };
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "Match Deleted" };
        }
    }
}
