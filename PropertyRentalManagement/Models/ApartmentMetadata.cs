using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class ApartmentMetadata
    {
        [Required]
        [Display(Name = "Apartment Number")]
        public int ApartmentNum { get; set; }

        [Required]
        [Display(Name = "Building Code")]
        public string BuildingCode { get; set; }

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int Bathrooms { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Rent { get; set; }

        [Required]
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }
    }
}