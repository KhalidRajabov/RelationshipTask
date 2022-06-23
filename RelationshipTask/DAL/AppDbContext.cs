using Microsoft.EntityFrameworkCore;
using RelationshipTask.Models;
using System.Text.RegularExpressions;

namespace RelationshipTask.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Groups> Groups { get; set; }


    }
}