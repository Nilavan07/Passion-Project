namespace WebApplication4.Dtos
{
    public class TeamCreateDto
    {
        public string Name { get; set; } = string.Empty; // Team name
        public string Coach { get; set; } = string.Empty; // Coach name
        public string Stadium { get; set; } = string.Empty; // Stadium name
        public int FoundedYear { get; set; } // Year founded
    }
}
