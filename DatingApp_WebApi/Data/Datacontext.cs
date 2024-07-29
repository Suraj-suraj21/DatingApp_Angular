
using AngularAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Data
{
    public class Datacontext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> users { get; set; }
    }
}
