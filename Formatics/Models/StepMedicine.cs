using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class StepMedicine
    {

        [Key]
        public int StepMedicineId { get; set; }

        [ForeignKey("Steps")]
        public int StepId { get; set; }
        public Steps Steps { get; set; }

        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}