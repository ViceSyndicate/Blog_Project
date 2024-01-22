using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.DataAccess
{
    public class EFBlogContext : IdentityDbContext<Models.User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbConn dbConn = new DbConn();
            optionsBuilder.UseSqlServer(dbConn.connectionString);
        }
        public DbSet<Models.VMUser> VMUsers { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Post> Posts { get; set; }
    }
}
