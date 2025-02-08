namespace FootballHub1.Models;
public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public ICollection<Player>? Players { get; set; }
}
