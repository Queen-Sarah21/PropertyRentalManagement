using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class UserMetadata
    {
        [Required]
        //[RegularExpression(@"^\d{5}$", ErrorMessage = "Tenant ID must be exactly 5 digits.")]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }

}