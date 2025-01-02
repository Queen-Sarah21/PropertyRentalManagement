using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class StatusMetadata
    {
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }
        public string Description { get; set; }

    }
}