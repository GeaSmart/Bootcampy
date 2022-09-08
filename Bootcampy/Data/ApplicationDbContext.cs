using Bootcampy.Models;
using Microsoft.EntityFrameworkCore;

namespace Bootcampy.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
