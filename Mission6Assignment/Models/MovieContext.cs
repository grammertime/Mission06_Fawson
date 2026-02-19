using Microsoft.EntityFrameworkCore;

namespace Mission06_Fawson.Models
{
    // This class inherits from DbContext, making it the official session/bridge between our app and the SQLite database.
    public class MovieContext : DbContext
    {
        // The constructor accepts connection options (like our connection string from appsettings.json) 
        // and passes them up to the base DbContext class to handle the actual connection.
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        // DbSet properties represent the actual tables in our database. 
        // When we use _context.Movies in our Controller, Entity Framework translates that into a SQL query.
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}