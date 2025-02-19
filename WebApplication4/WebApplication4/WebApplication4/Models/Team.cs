using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballHub.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // ✅ Ensure `Id` is properly set as Primary Key

        [Required]
        public string Name { get; set; }
        public string Coach { get; set; } 
        public string Stadium { get; set; }
        public int FoundedYear { get; set; }

        public ICollection<Player>? Players { get; set; }
        public ICollection<Match>? HomeMatches { get; set; }
        public ICollection<Match>? AwayMatches { get; set; }
    }
}
