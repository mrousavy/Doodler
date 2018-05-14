using Microsoft.EntityFrameworkCore;

namespace DoodlerCore
{
    // TODO: Docs
    public class DoodlerContext : DbContext
    {
        public DoodlerContext()
        {
        }

        public DoodlerContext(DbContextOptions options)
            : base(options)
        {
        }

        public DoodlerContext(DbContextOptions options, string connectionString)
            : base(options)
        {
            ConnectionString = connectionString;
        }

        public DoodlerContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                // Demo Data
                optionsBuilder.UseSqlServer("Server=192.168.0.44;Database=Doodler;User=sa;Password=Password1!;Trusted_Connection=False;");
            } else
            {
                // Actual Data
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<DateAnswer>().HasBaseType<Answer>();
            modelBuilder.Entity<TextAnswer>().HasBaseType<Answer>();
            //modelBuilder.Entity<Answer>().HasKey(a => a.Id);

            //modelBuilder.Entity<Poll>().ToTable("Polls");
            //modelBuilder.Entity<Poll>().HasKey(p => p.Id);

            //modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<User>().HasKey(u => u.Id);

            //modelBuilder.Entity<Vote>().ToTable("Votes");
            //modelBuilder.Entity<Vote>().HasKey(v => v.Id);
        }

        public string ConnectionString { get; set; } = "";
        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
