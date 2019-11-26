 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Steps
    {
        [Key]
        public int StepId { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }


        [Display(Name = "Day")]
        public int day { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }


        [Display(Name = "Description")]
        public string description { get; set; }

    }
}