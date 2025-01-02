using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class LandlordMetadata
    {
        [Required]
        [Display(Name = "Landlord Id")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Landlord ID must be exactly 4 digits.")]
        public int LandlordId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format 123-234-5432.")]
        public string Phone { get; set; }
    }
}