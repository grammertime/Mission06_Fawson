using System.ComponentModel.DataAnnotations;

namespace Mission06_Fawson.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string Rating { get; set; } // We will use a dropdown for this later (G, PG, PG-13, R)

        public bool Edited { get; set; } // "Yes/No" option

        public string? LentTo { get; set; } // Optional

        [StringLength(25)] // Limited to 25 characters
        public string? Notes { get; set; } // Optional
    }
}