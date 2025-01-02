using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class BuildingMetadata
    {
        [Required]
        [Display(Name = "Building Code")]
        public string BuildingCode { get; set; }

        [Display(Name = "Landlord Id")]
        public int LandlordId { get; set; }

        [Display(Name = "Manager Id")]
        public int ManagerId { get; set; }

        [Display(Name = "Building Name")]
        public string BuildingName { get; set; }

        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}