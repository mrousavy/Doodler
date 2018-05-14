using Microsoft.EntityFrameworkCore;

namespace DoodlerCore
{
    // TODO: Docs
    public class DoodlerContext : DbContext
    {
        public DoodlerContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public string ConnectionString { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
