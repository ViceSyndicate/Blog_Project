using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class EFBlogContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Models.User> Users { get; set; } = null!;
        public DbSet<Models.Post> Posts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbConn dbConn = new DbConn();
            optionsBuilder.UseSqlServer(dbConn.connectionString);
        }
    }
}
