using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FootballHub1.Models;

public class FootballHub1Context : IdentityDbContext
{
    public FootballHub1Context(DbContextOptions<FootballHub1Context> options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
}
