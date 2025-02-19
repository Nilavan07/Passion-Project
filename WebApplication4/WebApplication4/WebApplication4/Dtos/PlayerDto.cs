namespace FootballHub.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }  // Player ID
        public string Name { get; set; } = string.Empty; // Player name
        public string Position { get; set; } = string.Empty; // Playing position (e.g., Forward, Midfielder)
        public int Age { get; set; } // Age of the player
        public int TeamId { get; set; } // Foreign key for Team
    }
}
