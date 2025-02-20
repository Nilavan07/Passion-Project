namespace WebApplication4.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }  // Team ID
        public string Name { get; set; } = string.Empty; // Team name
        public string Coach { get; set; } = string.Empty; // Coach name
        public string Stadium { get; set; } = string.Empty; // Stadium name
        public int FoundedYear { get; set; } // Year founded
    }
}
