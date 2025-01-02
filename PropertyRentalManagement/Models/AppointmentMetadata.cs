using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class AppointmentMetadata
    {
        [Required]
        [Display(Name = "Appointment Id")]
        public int AppointmentId { get; set; }

        [Required]
        [Display(Name = "Tenant Id")]
        public int TenantId { get; set; }
        [Required]
        [Display(Name = "Manager Id")]
        public int ManagerId { get; set; }

        [Required]
        [Display(Name = "Apartment Number")]
        public int ApartmentNum { get; set; }

        [Required]
        [Display(Name = "Schedule Id")]
        public int ScheduleId { get; set; }
        [Required]
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }

    }
}