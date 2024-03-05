using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;

namespace DataLibrary
{
    public class EFBlogContextFactory : IDesignTimeDbContextFactory<EFBlogContext>
    {
        public EFBlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFBlogContext>();
            optionsBuilder.UseSqlServer(); //Connectionstringen behövs inte för att generera en vy

            return new EFBlogContext(optionsBuilder.Options);
        }
        EFBlogContext IDesignTimeDbContextFactory<EFBlogContext>.CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
