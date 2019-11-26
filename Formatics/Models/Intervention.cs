using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Intervention
    {

        [Key]
        public int InterventionId { get; set; }


        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime? endDate { get; set; }

        [Display(Name = "Duration")]
        public int duration { get; set; }  //in days


        [Display(Name = "Expected Outcome")]
        public string expectedOutcome { get; set; }  //improve or sustain

    }
}