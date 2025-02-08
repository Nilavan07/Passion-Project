namespace FootballHub1.Models;
public class Match
{
    public int MatchId { get; set; }
    public int HomeTeamId { get; set; }
    public Team? HomeTeam { get; set; }
    public int AwayTeamId { get; set; }
    public Team? AwayTeam { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
}
