// namespace WebApplication4.Dtos
// {
//     public class PlayerDto
//     {
//         public int Id { get; set; }  // Player ID
//         public string Name { get; set; } = string.Empty; // Player name
//         public string Position { get; set; } = string.Empty; // Playing position (e.g., Forward, Midfielder)
//         public int Age { get; set; } // Age of the player
//         public int TeamId { get; set; } // Foreign key for Team
//     }
// }

namespace WebApplication4.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }  // Player ID
        public string Name { get; set; } = string.Empty; // Player name
        public string Position { get; set; } = string.Empty; // Playing position (e.g., Forward, Midfielder)
        public int Age { get; set; } // Age of the player
        public int GoalsScored { get; set; } // ✅ New: Total goals scored
        public int Assists { get; set; } // ✅ New: Total assists
        public int TeamId { get; set; } // Foreign key for Team
        public string TeamName { get; set; } = string.Empty; // ✅ New: Display the team name
    }
}
