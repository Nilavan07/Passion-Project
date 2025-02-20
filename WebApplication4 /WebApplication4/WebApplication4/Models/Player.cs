using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballHub.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // ✅ Ensure `Id` is properly set

        [Required]
       public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int Age { get; set; }
    public int GoalsScored { get; set; } // ✅ New
    public int Assists { get; set; } // ✅ New
    public int TeamId { get; set; }
    public Team Team { get; set; } // ✅ Ensure navigation to Team
    }
}
