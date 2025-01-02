using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class ManagerMetadata
    {
        [Required]
        [Display(Name = "Manager Id")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Manager ID must be exactly 5 digits.")]
        public int ManagerId { get; set; }

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