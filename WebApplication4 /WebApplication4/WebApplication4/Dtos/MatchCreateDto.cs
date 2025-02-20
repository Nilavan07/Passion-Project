namespace WebApplication4.Dtos
{
    public class MatchCreateDto
    {
        public int HomeTeamId { get; set; } // ✅ Ensure this exists
        public int AwayTeamId { get; set; } // ✅ Ensure this exists
        public int HomeTeamScore { get; set; } // Home team goals
        public int AwayTeamScore { get; set; } // Away team goals
        public DateTime MatchDate { get; set; } // Date of match
    }
}

