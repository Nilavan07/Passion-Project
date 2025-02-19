namespace FootballHub.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; } // Match ID
        public int HomeTeamId { get; set; } // Foreign key for Home Team
        public int AwayTeamId { get; set; } // Foreign key for Away Team
        public int HomeTeamScore { get; set; } // Home team goals
        public int AwayTeamScore { get; set; } // Away team goals
        public DateTime MatchDate { get; set; } // Date of match
    }
}
