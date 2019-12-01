using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("Steps")]
        public int StepId { get; set; }
        public Steps Steps { get; set; }

        [ForeignKey("Patient")]
        public int PatientNumber { get; set; }
        public Patient Patient { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }

        [Display(Name = "Rating")]
        public int rating { get; set; } //1-10 daily, weekly month according to plan

        [Display(Name = "Comments")]
        public string comments { get; set; }
        
        [Display(Name = "Date Started")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; } //time of day sent out


    }
}