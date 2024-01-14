using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    internal class EFBlogContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbConn dbConn = new DbConn();
            optionsBuilder.UseSqlServer(dbConn.connectionString);
        }
    }
}
