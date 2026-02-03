using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Readify.Models
{
    public class Book
    {
        public int intBookId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string strTitle { get; set; } = string.Empty;

        [Required]
        [DisplayName("Author")]
        public string strAuthor { get; set; } = string.Empty;

        [Required]
        [DisplayName("Category")]
        public int? intCategoryId { get; set; }

        [Range(0, 10000)]
        [DisplayName("Price")]
        public decimal dclPrice { get; set; }

        public string? strImageUrl { get; set; }

        public DateTime dtmCreatedDateTime { get; set; } = DateTime.Now;

        public Category? Category { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
