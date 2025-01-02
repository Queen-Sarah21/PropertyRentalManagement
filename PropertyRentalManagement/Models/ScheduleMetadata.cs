using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class ScheduleMetadata
    {
        [Required]
        [Display(Name = "Schedule Id")]
        public int ScheduleId { get; set; }
        [Required]
        [Display(Name = "Manager Id")]
        public int ManagerId { get; set; }

        [Display(Name = "Schedule Date")]
        public System.DateTime ScheduleDate { get; set; }
        [Display(Name = "Start Time")]
        public System.TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        public System.TimeSpan EndTime { get; set; }
    }
}