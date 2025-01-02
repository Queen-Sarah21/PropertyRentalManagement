using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class ApartmentDetailsViewModel
    {
        public int ApartmentNum { get; set; }
        public string BuildingCode { get; set; }
        public string BuildingName { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Description { get; set; }
        public decimal Rent { get; set; }
        public int StatusId { get; set; }

        public string StatusDescription { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}