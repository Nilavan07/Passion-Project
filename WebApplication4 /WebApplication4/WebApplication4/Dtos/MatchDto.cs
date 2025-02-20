namespace WebApplication4.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; } // Match ID
        public string HomeTeamName { get; set; } = string.Empty; // ✅ Display Home Team Name
        public string AwayTeamName { get; set; } = string.Empty; // ✅ Display Away Team Name
        public int HomeTeamScore { get; set; } // Home team goals
        public int AwayTeamScore { get; set; } // Away team goals
        public DateTime MatchDate { get; set; } // Date of match
    }
}
