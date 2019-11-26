using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Alert
    {
        //A set amount of alert objects will be created for every intervention
        [Key]
        public int AlertId { get; set; }


        [Display(Name = "Type")]
        public string type { get; set; }


        [Display(Name = "Frequency")] //If applicable and not on condition.
        public int frequency { get; set; }

        [Display(Name = "Time")]
        public DateTime time { get; set; } //time of day sent out


        [Display(Name = "Description")] //If applicable and not on condition.
        public string description { get; set; }



    }
}