using System.ComponentModel.DataAnnotations;

namespace Mission06_Fawson.Models
{
    // Making a class of Category since there is a new table for Category in the database
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}