using Microsoft.EntityFrameworkCore;
using GFapi.Models;

namespace GFapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<AdminGold> Admins { get; set; }
    }
}