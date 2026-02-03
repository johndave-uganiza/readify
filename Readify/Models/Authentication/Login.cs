using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Readify.Models.Authentication
{
    public class Login
    {
        [Required]
        [DisplayName("Username")]
        public string strUsername { get; set; } = string.Empty;
        [Required]
        [DisplayName("Password")]
        public string strPassword { get; set; } = string.Empty;
    }
}
