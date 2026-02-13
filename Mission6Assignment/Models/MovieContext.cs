using Microsoft.EntityFrameworkCore;

namespace Mission06_Fawson.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Category = "Action/Sci-Fi",
                    Title = "Inception",
                    Year = 2010,
                    Director = "Christopher Nolan",
                    Rating = "PG-13",
                    Edited = false,
                    LentTo = "",
                    Notes = "Mind-bending movie"
                },
                new Movie
                {
                    MovieId = 2,
                    Category = "Comedy",
                    Title = "Fantastic Mr. Fox",
                    Year = 2009,
                    Director = "Wes Anderson",
                    Rating = "PG",
                    Edited = false,
                    LentTo = "",
                    Notes = "My favorite"
                },
                new Movie
                {
                    MovieId = 3,
                    Category = "Comedy",
                    Title = "Surf's Up",
                    Year = 2007,
                    Director = "Ash Brannon",
                    Rating = "PG",
                    Edited = false,
                    LentTo = "",
                    Notes = "Inspiring story"
                }
            );
        }
    }
}