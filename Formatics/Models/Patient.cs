using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class Patient
    {
     

            [Key]
            public int PatientNumber { get; set; }


            [ForeignKey("ApplicationUser")]
            public string ApplicationId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }


            [Display(Name = "First Name")]
            public string firstName { get; set; }

            [Display(Name = "Middle Name")]
            public string middleName { get; set; }

            [Display(Name = "Last Name")]
            public string lastName { get; set; }

            [Display(Name = "Age")]
            public int age { get; set; }

            [Display(Name = "Sex")]
            public string sex { get; set; }


            [Display(Name = "Phone Number")]
            public string phoneNumber { get; set; }

            [Display(Name = "Street Address")]
            public string streetAddress { get; set; }


            [Display(Name = "City")]
            public string city { get; set; }

            [Display(Name = "Zipcode")]
            public string zipcode { get; set; }

            [Display(Name = "State")]
            public string state { get; set; }


            [Display(Name = "Country")]
            public string country { get; set; }


            [Display(Name = "Date Enrolled")]
            [DataType(DataType.DateTime)]
            public DateTime enrollDate { get; set; }

            [Display(Name = "Insurance")]
            public string insurance { get; set; }

        
    }
}