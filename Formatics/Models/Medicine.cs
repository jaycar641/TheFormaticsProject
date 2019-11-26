using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }


        [Display(Name = "Drug Class")]
        public string drugClass { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display( Name = "Ingredients")]
        public List<string> ingredients { get; set; }

        [Display(Name = "Symptoms")]
        public List<string> symptoms { get; set; }

      

    }
}