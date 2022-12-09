using System.Data.Entity;

namespace GameAnalytics.Model
{
    public class GameContext : DbContext
    {
        public DbSet<GamesAnalytics> GamesAnalytics { get; set; }
        public DbSet<Users> Users { get; set; }

        public GameContext() : base("DefaultConnection")
        {

        }
    }
}
