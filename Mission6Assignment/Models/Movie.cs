using System.ComponentModel.DataAnnotations;

namespace Mission06_Fawson.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Navigation property

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1888, 3000, ErrorMessage = "Year must be 1888 or later")] // Validation is added to prevent false entries
        public int Year { get; set; }

        public string? Director { get; set; } // Optional, user doesn't have to enter it

        public string? Rating { get; set; } // Optional, user doesn't have to enter it

        [Required(ErrorMessage = "Edited field is required")]
        public bool Edited { get; set; }

        public string? LentTo { get; set; }

        [StringLength(25)]
        public string? Notes { get; set; }

        // NEW FIELD ADDED
        [Required(ErrorMessage = "Copied to Plex is required")]
        public bool CopiedToPlex { get; set; }
    }
}