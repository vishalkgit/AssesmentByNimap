using System.ComponentModel.DataAnnotations;

namespace AssesmentByNimap.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }

    }
}
