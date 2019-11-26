using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Procedure
    {

        [Key]
        public int ProcedureId { get; set; }

        [Display(Name = "Procedure Date")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Category")]
        public string category { get; set; }

       
    }
}