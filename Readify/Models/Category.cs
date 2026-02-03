using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Readify.Models
{
    public class Category
    {
        public int intCategoryId { get; set; }
        [DisplayName("Subject")]
        public string strSubject { get; set; } = string.Empty;

        [DisplayName("Volume Number")]
        [Range(1, 10, ErrorMessage = "The range is 1 to 10!")]
        public int intVolumeNumber { get; set; }
        public DateTime dtmCreatedDateTime { get; set; } = DateTime.Now;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
