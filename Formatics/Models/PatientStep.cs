using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class PatientStep
    {
        [Key]
        public int PatientStepId { get; set; }

        [ForeignKey("Steps")]
        public int StepId { get; set; }
        public Steps Steps { get; set; }

        [ForeignKey("Patient")]
        public int PatientNumber { get; set; }
        public Patient Patient { get; set; }


        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}