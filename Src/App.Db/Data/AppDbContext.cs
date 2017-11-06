using Microsoft.EntityFrameworkCore;
using App.Core.Entities;

namespace App.Db.Data
{
    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Match> Matches { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Round> Rounds { get; set; }
  }
}