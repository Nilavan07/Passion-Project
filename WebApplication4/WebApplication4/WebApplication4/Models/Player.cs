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
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }  // ✅ Foreign key for Team
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }  // ✅ Navigation property
    }
}
