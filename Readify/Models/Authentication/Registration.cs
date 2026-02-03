using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Readify.Models.Authentication
{
    public class Registration
    {
        [Required]
        [DisplayName("Name")]
        public string strName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string strEmail { get; set; } = string.Empty;
        [Required]
        [DisplayName("Username")]
        public string strUsername { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$"
            , ErrorMessage = "Password must be at least 6 characters long and contain at least 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character.")]
        [DisplayName("Password")]
        public string strPassword { get; set; } = string.Empty;
        [Required]
        [Compare("strPassword")]
        [DisplayName("Confirm Password")]
        public string strPasswordConfirm { get; set; } = string.Empty;
        public string strRole { get; set; } = string.Empty;
    }
}
