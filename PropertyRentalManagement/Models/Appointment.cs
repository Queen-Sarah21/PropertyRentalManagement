//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyRentalManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int TenantId { get; set; }
        public int ManagerId { get; set; }
        public int ApartmentNum { get; set; }
        public int ScheduleId { get; set; }
        public int StatusId { get; set; }
    
        public virtual Apartment Apartment { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Status Status { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}