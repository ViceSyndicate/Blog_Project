using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLibrary.DataAccess
{
    public class EFBlogContext : IdentityDbContext<IdentityUser>
    {
        public EFBlogContext(DbContextOptions options) : base(options)
        {
        }

        public EFBlogContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbConn dbConn = new DbConn();
            optionsBuilder.UseSqlServer(dbConn.connectionString);
        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //public DbSet<Models.VMUser> VMUsers { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Post> Posts { get; set; }
    }
}