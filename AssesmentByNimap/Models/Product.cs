using System.ComponentModel.DataAnnotations;

namespace AssesmentByNimap.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }

        public int CategoryId { get; set; }

      public string?CategoryName { get; set; }
    }
}
