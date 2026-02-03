using System.ComponentModel.DataAnnotations;

namespace Readify.Models
{
    public class Rental
    {
        public int intRentalId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int intBookId { get; set; }
        public int intUserId { get; set; }
        public string? strUserName { get; set; }
        public decimal dclTotalPrice { get; set; }

        [Required]
        public DateTime dtmRentalDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime dtmReturnDate { get; set; }
        public string strPaymentMethod { get; set; } = string.Empty;
        public bool ysnPaid { get; set; }
        public bool ysnReturned { get; set; }

        public Book? Book { get; set; }
    }
}
