using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRentalManagement.Models
{
    public class MessageMetadata
    {
        [Required]
        [Display(Name = "Message Id")]
        public int MessageId { get; set; }
        [Required]
        [Display(Name = "Sender Id")]
        public int SenderId { get; set; }
        [Required]
        [Display(Name = "Receiver Id")]
        public int ReceiverId { get; set; }

        [Required]
        [Display(Name = "Message Subject")]
        public string MessageSubject { get; set; }
        [Required]
        [Display(Name = "Message Body")]
        public string MessageBody { get; set; }
        [Required]
        [Display(Name = "Message Date")]
        public System.DateTime MessageDate { get; set; }
        [Required]
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }
    }
}