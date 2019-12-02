using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisId { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }

        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Date Diagnosed")]
        [DataType(DataType.DateTime)]
        public DateTime dateDiagnosed { get; set; }

        [Display(Name = "Is Current")]
        public bool isCurrent { get; set; }

    }
}