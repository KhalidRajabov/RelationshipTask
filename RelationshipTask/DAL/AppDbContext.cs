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
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Socials> Socials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

    }
}